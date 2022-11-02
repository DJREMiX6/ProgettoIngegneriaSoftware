using ProgettoIngegneriaSoftware.Utils.Random;

namespace ProgettoIngegneriaSoftware.Services
{
    public class TokenGeneratorService : ITokenGeneratorService
    {

        #region PRIVATE READONLY DI FIELDS

        private readonly ILogger<TokenGeneratorService> _logger;

        #endregion PRIVATE READONLY DI FIELDS

        #region CTORS

        public TokenGeneratorService(ILogger<TokenGeneratorService> logger)
        {
            _logger = logger;
        }

        #endregion CTORS

        #region ITokenGeneratorService IMPLEMENTATION

        public string GenerateToken(int length)
        {
            if(length <= 0)
            {
                throw new ArgumentException("length parameter is not valid.", nameof(length));
            }
            return RandomStringGenerator.Shared.RandomString(length);
        }

        #endregion ITokenGeneratorService IMPLEMENTATION

    }
}
