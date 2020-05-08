namespace System
{
  /// <summary>
  /// Builder for a random string generator.
  /// </summary>
  public interface IRandomStringGenerationBuilder
  {
    /// <summary>
    /// Sets the length of the random string to the specified value.
    /// </summary>
    /// <param name="stringLength">The desired length of the random string.</param>
    /// <returns>The builder.</returns>
    IRandomStringGenerationBuilder WithLength(int stringLength);

    /// <summary>
    /// Sets the collection of allowed characters to choose from during string generation.
    /// </summary>
    /// <param name="characters">The allowed characters.</param>
    /// <returns>The builder.</returns>
    IRandomStringGenerationBuilder AllowCharacters(params char[] characters);

    /// <summary>
    /// Sets the collection of allowed characters to choose from during string generation by using pre-defined character groups.
    /// </summary>
    /// <param name="characters">The allowed characters.</param>
    /// <returns>The builder.</returns>
    IRandomStringGenerationBuilder AllowCharacters(CharacterGroups characters);

    /// <summary>
    /// Adds additional allowed characters to the allowed characters given to <see cref="AllowCharacters(char[])"/>.
    /// </summary>
    /// <param name="characters">The additional allowed characters.</param>
    /// <returns>The builder.</returns>
    IRandomStringGenerationBuilder AndAllowCharacters(params char[] characters);

    /// <summary>
    /// Adds additional allowed characters to the allowed characters given to <see cref="AllowCharacters(char[])"/>.
    /// </summary>
    /// <param name="characters">The additional allowed characters.</param>
    /// <returns>The builder.</returns>
    IRandomStringGenerationBuilder AndAllowCharacters(CharacterGroups characters);

    /// <summary>
    /// Configures the random string generation so that each allowed character must occur at least once.
    /// </summary>
    /// <returns>The builder.</returns>
    IRandomStringGenerationBuilder EachCharacterMustOccurAtLeastOnce();

    /// <summary>
    /// Exclude characters from the use during the random string generation.
    /// </summary>
    /// <param name="characters">The excluded characters.</param>
    /// <returns>The builder.</returns>
    IRandomStringGenerationBuilder ExcludeCharacters(params char[] characters);

    /// <summary>
    /// Exclude similar looking characters from the use during the random string generation.
    /// Similar looking characters are: 
    /// 'l' (lower-case L), '1' (one), '|' (pipe), 'I' (upper-case i),
    /// '0' (zero), 'O' (upper-case o), 'o' (lower-case O).
    /// </summary>
    /// <returns>The builder.</returns>
    IRandomStringGenerationBuilder ExcludeSimilarLookingCharacters();
  }
}
