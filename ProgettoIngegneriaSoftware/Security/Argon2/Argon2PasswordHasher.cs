using Konscious.Security.Cryptography;
using ProgettoIngegneriaSoftware.Security.Extensions;
using ProgettoIngegneriaSoftware.Utils.Extensions;
using System.Text;
using System.Security.Cryptography;

namespace ProgettoIngegneriaSoftware.Security.Argon2
{
    public class Argon2PasswordHasher : IPasswordHasher
    {

        #region CONSTANTS

        private const int SALT_LENGTH = 32;
        private const int HASH_LENGTH = 128;

        #endregion CONSTANTS

        #region DI READONLY FIELDS

        private readonly ILogger<Argon2PasswordHasher> _logger;
        private readonly IPasswordHasherOptions _passwordHasherOptions;

        #endregion DI READONLY FIELDS

        #region CTORS

        public Argon2PasswordHasher(ILogger<Argon2PasswordHasher> logger, IPasswordHasherOptions passwordHasherOptions)
        {
            _logger = logger;
            _passwordHasherOptions = passwordHasherOptions;
        }

        #endregion CTORS

        #region IPasswordHasher IMPLEMENTATION

        public async Task<HashResult> HashPassword(string plainPassword)
        {
            return await Hash(plainPassword, null, false);
        }

        public async Task<HashResult> HashPassword(string plainPassword, byte[]? salt, bool useSecret = false)
        {
            return await Hash(plainPassword, salt, true);
        }

        public async Task<bool> VerifyPassword(string plainPassword, byte[] hashedPassword, byte[]? salt, bool useSecret = false)
        {
            HashResult result = await Hash(plainPassword, salt, useSecret);
            return result.Hash.EqualsByByte(hashedPassword);
        }

        #endregion IPasswordHasher IMPLEMENTATION

        #region PRIVATE METHODS

        private async Task<HashResult> Hash(string plainPassword, byte[]? salt, bool useSecret = false)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(plainPassword);
            var argon2id = new Argon2id(passwordBytes);

            byte[] saltBytes = new byte[SALT_LENGTH];
            if (salt != null)
            {
                if(salt.Length != SALT_LENGTH)
                {
                    throw new ArgumentException($"Salt length is different than {SALT_LENGTH}.", nameof(salt));
                }
                saltBytes = salt;
            }
            else
            {
                saltBytes = RandomNumberGenerator.GetBytes(SALT_LENGTH);
            }

            argon2id.DegreeOfParallelism = _passwordHasherOptions.GetDegreeOfParallelism() is null ?
                throw new ArgumentNullException("_passwordHasherOptions.GetDegreeOfParallelism() is null")
                : Convert.ToInt32(_passwordHasherOptions.GetDegreeOfParallelism());
            argon2id.MemorySize = _passwordHasherOptions.GetMemorySize() is null ?
                throw new ArgumentNullException("_passwordHasherOptions.GetMemorySize() is null")
                : Convert.ToInt32(_passwordHasherOptions.GetMemorySize());
            argon2id.Iterations = _passwordHasherOptions.GetIterationsOption() is null ?
                throw new ArgumentNullException("_passwordHasherOptions.GetIterationsOption() is null")
                : Convert.ToInt32(_passwordHasherOptions.GetIterationsOption());
            argon2id.Salt = saltBytes;
            if (useSecret)
            {
                argon2id.KnownSecret = _passwordHasherOptions.GetKnownSecretOption() is null ?
                    throw new ArgumentNullException("_passwordHasherOptions.GetKnownSecretOption() is null")
                    : Encoding.UTF8.GetBytes(_passwordHasherOptions.GetKnownSecretOption());
            }

            return new HashResult(await argon2id.GetBytesAsync(HASH_LENGTH), saltBytes);
        }

        #endregion PRIVATE METHODS

    }
}
