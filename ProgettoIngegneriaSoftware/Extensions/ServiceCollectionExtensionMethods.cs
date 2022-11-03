using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models;
using ProgettoIngegneriaSoftware.Security;
using ProgettoIngegneriaSoftware.Security.Argon2;
using ProgettoIngegneriaSoftware.Services;

namespace ProgettoIngegneriaSoftware.Extensions
{
    public static class ServiceCollectionExtensionMethods
    {

        public static IServiceCollection AddDatabaseContext(this IServiceCollection serviceCollection, IConfiguration configuration, bool useTempDb = false) 
        {
            if (useTempDb)
            {
                serviceCollection.AddDbContext<AuthenticationDbContext>(options => options.UseInMemoryDatabase("Test_ProgettoIngegneria_AutenticationDb"));
            }
            else
            {
                serviceCollection.AddDbContext<AuthenticationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("AutenticationDefaultConnection")));
            }

            serviceCollection.AddDatabaseDeveloperPageExceptionFilter();

            return serviceCollection;
        }

        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserModelService, UserModelService>();
            serviceCollection.AddTransient<ILoginTokenModelService, LoginTokenModelService>();
            serviceCollection.AddTransient<ITokenGeneratorService, TokenGeneratorService>();

            return serviceCollection;
        }

        public static IServiceCollection AddPasswordHasher(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPasswordHasher, Argon2PasswordHasher>();
            serviceCollection.AddTransient<IPasswordHasherOptions, Argon2PasswordHasherOptions>();

            return serviceCollection;
        }
    }
}
