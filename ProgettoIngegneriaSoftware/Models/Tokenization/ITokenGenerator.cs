namespace ProgettoIngegneriaSoftware.Models.Tokenization
{
    /// <summary>
    /// Abstraction for Tokens generation
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generates a Token synchronously
        /// </summary>
        /// <returns>The <c>string</c> Token</returns>
        public string GenerateToken(int length);
    }
}
