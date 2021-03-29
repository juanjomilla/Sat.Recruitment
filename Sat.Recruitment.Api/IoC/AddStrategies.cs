using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Strategies;

namespace Sat.Recruitment.Api.IoC
{
    public static class AddStrategies
    {
        public static void AddUserTypeStrategies(this IServiceCollection services)
        {
            services.AddSingleton<INormalUserStrategy>(serviceProvider => { return new NormalUserStrategy(); });
            services.AddSingleton<ISuperUserStrategy>(serviceProvider => { return new SuperUserStrategy(); });
            services.AddSingleton<IPremiumUserStrategy>(serviceProvider => { return new PremiuUserStrategy(); });
        }
    }
}
