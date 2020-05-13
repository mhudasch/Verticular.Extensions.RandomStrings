namespace System
{
  /// <summary>
  /// Defines the functionality to generate random strings.
  /// </summary>
  public interface IRandomStringGenerator
  {
    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="length">The desired length of the generated string.</param>
    /// <param name="allowedCharacters">The set of allowed characters used for string generation.</param>
    /// <param name="eachCharacterMustOccurAtLeastOnce">Specifies whether each character in the <paramref name="allowedCharacters"/>
    /// array must occur at least once in the generated random string.</param>
    /// <returns>The generated string.</returns>
    string Generate(int length, char[] allowedCharacters, bool eachCharacterMustOccurAtLeastOnce = false);

    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="length">The desired length of the generated string.</param>
    /// <param name="allowedCharacters">The set of allowed characters used for string generation.</param>
    /// <param name="eachCharacterMustOccurAtLeastOnce">Specifies whether each character in the <paramref name="allowedCharacters"/>
    /// array must occur at least once in the generated random string.</param>
    /// <returns>The generated string.</returns>
    string Generate(int length, CharacterGroups allowedCharacters, bool eachCharacterMustOccurAtLeastOnce = false);

    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="length">The desired length of the generated string.</param>
    /// <returns>The generated string.</returns>
    string Generate(int length);

    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="builder">The builder that defines the options for generating a random string.</param>
    /// <returns>The generated string.</returns>
    string Generate(Action<IRandomStringGenerationBuilder> builder);

    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="options">The options for generating a random string.</param>
    /// <returns>The generated string.</returns>
    string Generate(RandomStringGenerationOptions options);

    /// <summary>
    /// Generates a string using the default options from <see cref="RandomStringGenerationOptions.Default"/>.
    /// </summary>
    /// <returns>The generated string.</returns>
    string Generate();
  }
}