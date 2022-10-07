namespace ProgettoIngegneriaSoftware.Security
{
    /// <summary>
    /// Provides an abstraction for password hashing and validation classes
    /// </summary>
    public interface IPasswordHasher
    {

        /// <summary>
        /// Hashes a given password generating a random salt
        /// </summary>
        /// <param name="plainPassword">The password to hash</param>
        /// <returns>the password's hash</returns>
        public Task<HashResult> HashPassword(string plainPassword);

        /// <summary>
        /// Hashes a given password adding salt and eventually the known secret to it
        /// </summary>
        /// <param name="plainPassword">The password to hash</param>
        /// <param name="salt">The salt added to the password. If <c>null</c> the algorithm will generate a random salt</param>
        /// <param name="useSecret">if <c>true</c> the algorithm will use the known secret to add another layer of security</param>
        /// <returns>the password's hash and salt</returns>
        public Task<HashResult> HashPassword(string plainPassword, byte[]? salt, bool useSecret = false);

        /// <summary>
        /// Verify if a given password matches an hashed password
        /// </summary>
        /// <param name="plainPassword">The plain password that will be hashed</param>
        /// <param name="hashedPassword">The hash of a password to match</param>
        /// <param name="salt">The salt used for the hashed password</param>
        /// <param name="useSecret">If <c>true</c> the algorithm will use the known secret</param>
        /// <returns><c>true</c> if the hash of <c>plainPassword</c> matches the <c>hashedPassword</c></returns>
        public Task<bool> VerifyPassword(string plainPassword, byte[] hashedPassword, byte[]? salt = null, bool useSecret = false);

    }
}
