using ProgettoIngegneriaSoftware.Middlewares.Test;
using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication.Records;

namespace ProgettoIngegneriaSoftware.Extensions
{
    public static class ApplicationBuilderExentionMethods
    {
        /// <summary>
        /// USE THIS FOR TESTING PURPOSE ONLY!!!
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDefaultAuthenticatedUser(this IApplicationBuilder applicationBuilder,
            UserModelRecord defaultAuthenticatedUser) =>
            applicationBuilder.UseMiddleware<DefaultAuthenticatedUserMiddleware>(defaultAuthenticatedUser);

        /// <summary>
        /// USE THIS FOR TESTING PURPOSE ONLY!!!
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDefaultAuthenticatedUser(this IApplicationBuilder applicationBuilder) =>
            applicationBuilder.UseDefaultAuthenticatedUser(new UserModelRecord()
            {
                Username = "FG666",
                Email = "email@example.com",
                Password = "FraGire6!",
                ConfirmPassword = "FraGire6!"
            });
    }
}
