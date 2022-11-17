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
                serviceCollection
                    .AddDbContext<AuthenticationDbContext>(options => options.UseInMemoryDatabase($"{assemblyName}_{nameof(AuthenticationDbContext)}"))
                    .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase($"{assemblyName}_{nameof(ApplicationDbContext)}"));
            }
            else
            {
                serviceCollection.AddDbContext<AuthenticationDbContext>(options =>
                    options.UseSqlServer(configuration.GetAuthenticationConnectionString()));
                serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetApplicationConnectionString()));
            }

            return serviceCollection;
        }

        public static IServiceCollection AddPasswordServices(this IServiceCollection serviceCollection) =>
            serviceCollection.AddTransient<IPasswordHasher, Argon2PasswordHasher>().
                AddTransient<IPasswordHasherOptions, Argon2PasswordHasherOptions>();

        public static IServiceCollection AddUserModelService(this IServiceCollection serviceCollection) =>
            serviceCollection.AddTransient<IUserModelService, UserModelService>();

        public static IServiceCollection AddUserAuthenticationService(this IServiceCollection serviceCollection) =>
            serviceCollection.AddTransient<IUserAuthenticationService, UserAuthenticationService>();

        public static IServiceCollection AddEventModelService(this IServiceCollection serviceCollection) =>
            serviceCollection.AddTransient<IEventModelService, EventModelService>();

        public static IServiceCollection AddConfirmationEmailSenderService(this IServiceCollection serviceCollection) =>
            serviceCollection.AddTransient<IConfirmationEmailSenderService, ConfirmationEmailSenderService>();

        public static IServiceCollection AddServices(this IServiceCollection serviceCollection) =>
            serviceCollection.AddUserModelService()
                .AddPasswordServices()
                .AddUserAuthenticationService()
                .AddEventModelService()
                .AddConfirmationEmailSenderService();

    }
}
