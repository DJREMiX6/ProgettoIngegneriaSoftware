namespace ProgettoIngegneriaSoftware.Extensions
{
    public static class ConfigurationExtensionMethods
    {

        /// <summary>
        /// Shorthand for <c>GetSection("Constants")</c>
        /// </summary>
        /// <param name="configuration"><seealso cref="IConfiguration"/> instance</param>
        /// <returns>the Constants <seealso cref="IConfigurationSection"/> instance</returns>
        public static IConfigurationSection GetConstants(this IConfiguration configuration) => 
            configuration.GetSection("Constants");

        /// <summary>
        /// Shorthand for <c>GetSection("Constants").GetSection("Security")</c>
        /// </summary>
        /// <param name="configuration"><seealso cref="IConfiguration"/> instance</param>
        /// <returns>the Security <seealso cref="IConfigurationSection"/> instance</returns>
        public static IConfigurationSection GetSecurityConstants(this IConfiguration configuration) => 
            configuration.GetConstants().GetSection("Security");

        /// <summary>
        /// Shorthand for <c>GetSection("Constants").GetSection("Security").GetValue("Secret")</c>
        /// </summary>
        /// <param name="configuration"><seealso cref="IConfiguration"/> instance</param>
        /// <returns>the Secret's value</returns>
        public static string GetSecuritySecret(this IConfiguration configuration) => 
            configuration.GetSecurityConstants().GetValue<string>("Secret");

        public static string GetAuthenticationConnectionString(this IConfiguration configuration) =>
            configuration.GetConnectionString("AuthenticationDefaultConnection");

        public static string GetApplicationConnectionString(this IConfiguration configuration) =>
            configuration.GetConnectionString("ApplicationDefaultConnection");

        public static IConfigurationSection GetSmtpEmailConfirmationSection(this IConfiguration configuration) =>
            configuration.GetSection("SmtpEmailConfirmation");

        public static string GetSmtpEmailConfirmationServer(this IConfiguration configuration) =>
            configuration.GetSmtpEmailConfirmationSection().GetValue<string>("Server");

        public static int GetSmtpEmailConfirmationPort(this IConfiguration configuration) =>
            configuration.GetSmtpEmailConfirmationSection().GetValue<int>("Port");

        public static string GetSmtpEmailConfirmationUsername(this IConfiguration configuration) =>
            configuration.GetSmtpEmailConfirmationSection().GetValue<string>("Username");

        public static string GetSmtpEmailConfirmationPassword(this IConfiguration configuration) =>
            configuration.GetSmtpEmailConfirmationSection().GetValue<string>("Password");

    }
}
