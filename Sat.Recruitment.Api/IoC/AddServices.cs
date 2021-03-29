using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Strategies;

namespace Sat.Recruitment.Api.IoC
{
    public static class AddServices
    {
        public static void AddUserService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService>(serviceProvider =>
            {
                var normalUserStrategy = serviceProvider.GetService<INormalUserStrategy>();
                var superUserStrategy = serviceProvider.GetService<ISuperUserStrategy>();
                var premiumUserStrategy = serviceProvider.GetService<IPremiumUserStrategy>();
                var csvRepository = serviceProvider.GetService<IRepositoryService>();

                if(normalUserStrategy == null || superUserStrategy == null || premiumUserStrategy == null)
                {
                    Debug.WriteLine("User Type strategies were not registered into the DI container");
                    throw new Exception("User Type strategies were not registered into the DI container");
                }

                if(csvRepository == null)
                {
                    Debug.WriteLine("CSV repository was not registered into the DI container");
                    throw new Exception("CSV repository was not registered into the DI container");
                }

                return new UserService(normalUserStrategy, premiumUserStrategy, superUserStrategy, csvRepository);
            });
        }
    }
}
