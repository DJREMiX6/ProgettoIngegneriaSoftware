namespace ProgettoIngegneriaSoftware.API.Security.Argon2.Extensions;

public static class Argon2PasswordHasherExtensions
{

    private const string DEGREE_OF_PARALLELISM_OPTION_NAME = "DegreeOfParallelism";
    private const string MEMORY_SIZE_OPTION_NAME = "MemorySize";
    private const string ITERATIONS_OPTION_NAME = "Iterations";
    private const string SALT_OPTION_NAME = "Salt";
    private const string ASSOCIATED_DATA_OPTION_NAME = "AssociatedData";
    private const string KNOWN_SECRET_OPTION_NAME = "KnownSecret";

    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>["DegreeOfParallelism"]
    /// </summary>
    /// <param name="options"></param>
    /// <returns>the option value or <c>null</c> if the option doesn't exists</returns>
    public static string? GetDegreeOfParallelism(this IPasswordHasherOptions options) => options[DEGREE_OF_PARALLELISM_OPTION_NAME];
    /// <summary>
    /// Sets the <c>DegreeOfParallelism</c> option
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The number of thread that the Argon2 algorithm will use</param>
    /// <returns><seealso cref="IPasswordHasherOptions"/> instance to chain the method</returns>
    public static IPasswordHasherOptions SetDegreeOfParallelism(this IPasswordHasherOptions options, uint value)
    {
        options.Options[DEGREE_OF_PARALLELISM_OPTION_NAME] = Convert.ToString(value);
        return options;
    }
    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>.TryAddOption("DegreeOfParallelism", value)
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The number of thread that the Argon2 algorithm will use</param>
    /// <returns>the result of <seealso cref="IPasswordHasherOptions"/>.TryAddOption("DegreeOfParallelism", value)</param></returns>
    public static bool TrySetDegreeOfParallelism(this IPasswordHasherOptions options, uint value) => options.TryAddOption(DEGREE_OF_PARALLELISM_OPTION_NAME, Convert.ToString(value));



    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>["MemorySize"]
    /// </summary>
    /// <param name="options"></param>
    /// <returns>the option value or <c>null</c> if the option doesn't exists</returns>
    public static string? GetMemorySize(this IPasswordHasherOptions options) => options[MEMORY_SIZE_OPTION_NAME];
    /// <summary>
    /// Sets the <c>MemorySize</c> option
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The amount memory in kiB that the Argon2 algorithm will use</param>
    /// <returns><seealso cref="IPasswordHasherOptions"/> instance to chain the method</returns>
    public static IPasswordHasherOptions SetMemorySize(this IPasswordHasherOptions options, uint value)
    {
        options.Options[MEMORY_SIZE_OPTION_NAME] = Convert.ToString(value);
        return options;
    }
    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>.TryAddOption("MemorySize", value)
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The amount of memory in kiB that the Argon2 algorithm will use</param>
    /// <returns>the result of <seealso cref="IPasswordHasherOptions"/>.TryAddOption("MemorySize", value)</param></returns>
    public static bool TrySetMemorySize(this IPasswordHasherOptions options, uint value) => options.TryAddOption(MEMORY_SIZE_OPTION_NAME, Convert.ToString(value));



    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>["Iterations"]
    /// </summary>
    /// <param name="options"></param>
    /// <returns>the option value or <c>null</c> if the option doesn't exists</returns>
    public static string? GetIterationsOption(this IPasswordHasherOptions options) => options[ITERATIONS_OPTION_NAME];
    /// <summary>
    /// Sets the <c>Iterations</c> option
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The number of iterations that the Argon2 algorithm will make</param>
    /// <returns><seealso cref="IPasswordHasherOptions"/> instance to chain the method</returns>
    public static IPasswordHasherOptions SetIterationsOption(this IPasswordHasherOptions options, uint value)
    {
        options.Options[ITERATIONS_OPTION_NAME] = Convert.ToString(value);
        return options;
    }
    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>.TryAddOption("Iterations", value)
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The number of iterations that the Argon2 algorithm will make</param>
    /// <returns>the result of <seealso cref="IPasswordHasherOptions"/>.TryAddOption("Iterations", value)</param></returns>
    public static bool TrySetIterationsOption(this IPasswordHasherOptions options, uint value) => options.TryAddOption(ITERATIONS_OPTION_NAME, Convert.ToString(value));



    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>["Salt"]
    /// </summary>
    /// <param name="options"></param>
    /// <returns>the option value or <c>null</c> if the option doesn't exists</returns>
    public static string? GetSaltOption(this IPasswordHasherOptions options) => options[SALT_OPTION_NAME];
    /// <summary>
    /// Sets the <c>Salt</c> option
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The salt that the Argon2 algorithm will add to the password</param>
    /// <returns><seealso cref="IPasswordHasherOptions"/> instance to chain the method</returns>
    public static IPasswordHasherOptions SetSaltOption(this IPasswordHasherOptions options, string value)
    {
        options.Options[SALT_OPTION_NAME] = value;
        return options;
    }
    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>.TryAddOption("Salt", value)
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The salt that the Argon2 algorithm will add to the password</param>
    /// <returns>the result of <seealso cref="IPasswordHasherOptions"/>.TryAddOption("Salt", value)</param></returns>
    public static bool TrySetSaltOption(this IPasswordHasherOptions options, string value) => options.TryAddOption(SALT_OPTION_NAME, value);



    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>["AssociatedData"]
    /// </summary>
    /// <param name="options"></param>
    /// <returns>the option value or <c>null</c> if the option doesn't exists</returns>
    public static string? GetAssociatedDataOption(this IPasswordHasherOptions options) => options[ASSOCIATED_DATA_OPTION_NAME];
    /// <summary>
    /// Sets the <c>AssociatedData</c> option
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The additional associated data that the Argon2 algorithm will use to add another security layer</param>
    /// <returns><seealso cref="IPasswordHasherOptions"/> instance to chain the method</returns>
    public static IPasswordHasherOptions SetAssociatedDataOption(this IPasswordHasherOptions options, string value)
    {
        options.Options[ASSOCIATED_DATA_OPTION_NAME] = value;
        return options;
    }
    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>.TryAddOption("AssociatedData", value)
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The additional associated data that the Argon2 algorithm will use to add another security layer</param>
    /// <returns>the result of <seealso cref="IPasswordHasherOptions"/>.TryAddOption("AssociatedData", value)</param></returns>
    public static bool TrySetAssociatedDataOption(this IPasswordHasherOptions options, string value) => options.TryAddOption(ASSOCIATED_DATA_OPTION_NAME, value);



    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>["KnownSecret"]
    /// </summary>
    /// <param name="options"></param>
    /// <returns>the option value or <c>null</c> if the option doesn't exists</returns>
    public static string? GetKnownSecretOption(this IPasswordHasherOptions options) => options[KNOWN_SECRET_OPTION_NAME];
    /// <summary>
    /// Sets the <c>KnownSecret</c> option
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The additional known secret that the Argon2 algorithm will use to add another security layer</param>
    /// <returns><seealso cref="IPasswordHasherOptions"/> instance to chain the method</returns>
    public static IPasswordHasherOptions SetKnownSecretOption(this IPasswordHasherOptions options, string value)
    {
        options.Options[KNOWN_SECRET_OPTION_NAME] = value;
        return options;
    }
    /// <summary>
    /// Shorthand for <seealso cref="IPasswordHasherOptions"/>.TryAddOption("KnownSecret", value)
    /// </summary>
    /// <param name="options"></param>
    /// <param name="value">The additional known secret that the Argon2 algorithm will use to add another security layer</param>
    /// <returns>the result of <seealso cref="IPasswordHasherOptions"/>.TryAddOption("KnownSecret", value)</param></returns>
    public static bool TrySetKnownSecretOption(this IPasswordHasherOptions options, string value) => options.TryAddOption(KNOWN_SECRET_OPTION_NAME, value);

}