namespace ProgettoIngegneriaSoftware.API.Security
{
    /// <summary>
    /// Abstraction for password hashing options
    /// </summary>
    public interface IPasswordHasherOptions
    {

        /// <summary>
        /// Represents a collection that holds pairs of key/value options
        /// </summary>
        public IDictionary<string, string> Options { get; }

        /// <summary>
        /// Tries to add an option to the <seealso cref="Options"/> dictionary
        /// </summary>
        /// <param name="key">The name of the option</param>
        /// <param name="value">The value of the option</param>
        /// <returns><c>true</c> if the add operation went successfully</returns>
        public bool TryAddOption(string key, string value);

        /// <summary>
        /// Shorthand for <seealso cref="Options"/>[key]
        /// </summary>
        /// <param name="key">The name of the option</param>
        /// <returns>the value of the option if its present in <seealso cref="Options"/> or <c>null</c> if its missing.</returns>
        string? this[string key] { get; }

    }
}
