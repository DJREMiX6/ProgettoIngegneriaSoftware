using Konscious.Security.Cryptography;
using ProgettoIngegneriaSoftware.Security.Extensions;
using System.Text;
using System.Security.Cryptography;

namespace ProgettoIngegneriaSoftware.Security.Argon2
{
    public class Argon2PasswordHasher : IPasswordHasher
    {

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

        public HashResult HashPassword(string plainPassword)
        {
            return Hash(plainPassword, null, false);
        }

        public HashResult HashPassword(string plainPassword, string? salt, bool useSecret = false)
        {
            return Hash(plainPassword, salt, true);
        }

        public bool VerifyPassword(string plainPassword, string hashedPassword, string? salt, bool useSecret = false)
        {
            HashResult result = Hash(plainPassword, salt, useSecret);
            return result.Hash.Equals(Encoding.UTF8.GetBytes(hashedPassword));
        }

        #endregion IPasswordHasher IMPLEMENTATION

        #region PRIVATE METHODS

        private HashResult Hash(string plainPassword, string? salt, bool useSecret = false)
        {
            try
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(plainPassword);
                var argon2id = new Argon2id(passwordBytes);

                byte[] saltBytes = new byte[0];
                if (salt != null)
                {
                    saltBytes = Encoding.UTF8.GetBytes(salt);
                }
                else
                {
                    saltBytes = RandomNumberGenerator.GetBytes(32);
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

                return new HashResult(argon2id.GetBytes(128), saltBytes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting options for hashing");
                return new HashResult();
            }
        }

        #endregion PRIVATE METHODS

    }
}
