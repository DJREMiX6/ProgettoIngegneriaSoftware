using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models;
using ProgettoIngegneriaSoftware.Security;
using ProgettoIngegneriaSoftware.Security.Argon2;
using ProgettoIngegneriaSoftware.Services;

namespace ProgettoIngegneriaSoftware.Extensions
{
    public static class ServiceCollectionExtensionMethods
    {

        public static IServiceCollection AddDatabaseContexts(this IServiceCollection serviceCollection, IConfiguration configuration, bool useTempDb = false) 
        {
            if (useTempDb)
            {
                var assemblyName = Assembly.GetEntryAssembly().FullName;
                serviceCollection.AddDbContext<AuthenticationDbContext>(options => options.UseInMemoryDatabase($"{assemblyName}_AuthenticationDB"));
            }
            else
            {
                serviceCollection.AddDbContext<AuthenticationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("AutenticationDefaultConnection")));
            }

            serviceCollection.AddDatabaseDeveloperPageExceptionFilter();

            return serviceCollection;
        }

        public static IServiceCollection AddServices(this IServiceCollection serviceCollection) =>
            serviceCollection.AddUserModelService()
                .AddPasswordHasher();

        public static IServiceCollection AddPasswordHasher(this IServiceCollection serviceCollection) =>
            serviceCollection.AddTransient<IPasswordHasher, Argon2PasswordHasher>().
                AddTransient<IPasswordHasherOptions, Argon2PasswordHasherOptions>();

        public static IServiceCollection AddUserModelService(this IServiceCollection serviceCollection) =>
            serviceCollection.AddTransient<IUserModelService, UserModelService>();

    }
}
