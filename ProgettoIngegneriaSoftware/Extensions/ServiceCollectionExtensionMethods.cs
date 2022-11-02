using Microsoft.EntityFrameworkCore;
using ProgettoIngegneriaSoftware.Models;
using ProgettoIngegneriaSoftware.Models.ControllersModels;
using ProgettoIngegneriaSoftware.Security;
using ProgettoIngegneriaSoftware.Security.Argon2;
using ProgettoIngegneriaSoftware.Services;

namespace ProgettoIngegneriaSoftware.Extensions
{
    public static class ServiceCollectionExtensionMethods
    {

        public static IServiceCollection AddDatabaseContext(this IServiceCollection serviceCollection)
        {

#if DEBUG
            serviceCollection.AddDbContext<AutenticationDbContext>(options => options.UseInMemoryDatabase("Test_ProgettoIngegneria_AutenticationDb"));
            serviceCollection.AddDatabaseDeveloperPageExceptionFilter();
#else
            serviceCollection.AddDbContext<AutenticationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AutenticationDefaultConnection")));
#endif

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

        public static IServiceCollection AddModels(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ILoginRegisterModel, LoginRegisterModel>();

            return serviceCollection;
        }
    }
}
