using NuGet.Common;
using System.Security.Cryptography;
using System.Text;

namespace ProgettoIngegneriaSoftware.Models.Tokenization
{
    public class TokenGenerator : ITokenGenerator
    {

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<TokenGenerator> _logger;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public TokenGenerator(ILogger<TokenGenerator> logger)
        {
            _logger = logger;
        }

        #endregion CTORS

        #region ITokenGenerator IMPLEMENTATION

        public string GenerateToken(int length)
        {
            if(length <= 0)
            {
                throw new ArgumentException("length parameter is not valid.", nameof(length));
            }

            var randomBytes = RandomNumberGenerator.GetBytes(length);
            return Encoding.UTF8.GetString(randomBytes);
        }

        #endregion ITokenGenerator IMPLEMENTATION

    }
}
