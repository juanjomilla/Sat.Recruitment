using System.Globalization;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api.Services;

namespace Sat.Recruitment.Api.IoC
{
    public static class AddRepository
    {
        public static void AddCsvRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IRepositoryService>(serviceProvider => {
                var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

                return new CsvRepositoryService(path, CultureInfo.InvariantCulture, false);
            });
        }
    }
}
