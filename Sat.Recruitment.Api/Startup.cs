using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sat.Recruitment.Api.IoC;
using Sat.Recruitment.Api.Models.Response;

namespace Sat.Recruitment.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddMvc().ConfigureApiBehaviorOptions(options => {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    return CustomValidationErrorResponse(actionContext);
                };
            });

            services.AddUserTypeStrategies();
            services.AddCsvRepository();
            services.AddUserService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private BadRequestObjectResult CustomValidationErrorResponse(ActionContext actionContext)
        {
            var errors = actionContext.ModelState
                .Where(modelError => modelError.Value.Errors.Count > 0)
                .Select(modelError => modelError.Value.Errors.FirstOrDefault().ErrorMessage);

            return new BadRequestObjectResult(new Result { IsSuccess = false, Errors = string.Join(", ", errors) });
        }
    }
}
