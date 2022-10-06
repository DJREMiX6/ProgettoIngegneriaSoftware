using ProgettoIngegneriaSoftware.Extensions;
using ProgettoIngegneriaSoftware.Security.Extensions;

namespace ProgettoIngegneriaSoftware.Security.Argon2
{
    public class Argon2PasswordHasherOptions : IPasswordHasherOptions
    {

        #region PRIVATE BACKING FIELDS

        private IDictionary<string, string> _options;

        #endregion PRIVATE BACKING FIELDS

        #region DI READONLY FIELDS

        private readonly ILogger<Argon2PasswordHasherOptions> _logger;
        private readonly IConfiguration _configuration;

        #endregion DI READONLY FIELDS

        #region CTORS

        public Argon2PasswordHasherOptions(ILogger<Argon2PasswordHasherOptions> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            ((IPasswordHasherOptions)this)
                .SetDegreeOfParallelism(6)
                .SetMemorySize(1024 * 1024)
                .SetIterationsOption(12)
                .SetKnownSecretOption(_configuration.GetSecuritySecret());
        }

        #endregion CTORS

        #region IPasswordHasherOptions IMPLEMENTATION

        public IDictionary<string, string> Options => _options ??= new Dictionary<string, string>();

        /// <summary>
        /// Tries to add an option to the <seealso cref="Options"/>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>
        ///     <c>true</c> if no problems were encountered during the operation.
        ///     <c>false</c> if the key already exists or if happened some unexpected exception that is being catched and logged with <c>LogLevel Error</c>
        /// </returns>
        public bool TryAddOption(string key, string value)
        {
            bool operationExecutedSuccessfully = true;
            if (Options.ContainsKey(key))
            {
                return false;
            }
            try
            {
                Options.Add(key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"key: {key}, value: {value}");
                operationExecutedSuccessfully = false;
            }
            return operationExecutedSuccessfully;
        }

        public string? this[string key] => TryGet(key);

        #endregion IPasswordHasherOptions IMPLEMENTATION

        #region PRIVATE METHODS

        /// <summary>
        /// Tries to get an option from <seealso cref="Options"/>
        /// </summary>
        /// <param name="key">The name of the option to get</param>
        /// <returns>the option value or <c>null</c> if the option was not found</returns>
        private string? TryGet(string key)
        {
            if (Options.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;
        }

        #endregion PRIVATE METHODS

    }
}
