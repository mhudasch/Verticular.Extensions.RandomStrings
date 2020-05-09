namespace System
{
  using Verticular.Extensions.RandomStrings;

  /// <summary>
  /// The options used to define how a random string will be created.
  /// </summary>
  public class RandomStringGenerationOptions
  {
    /// <summary>
    /// The default options for random string generation.
    /// </summary>
    public static readonly RandomStringGenerationOptions Default =
      new RandomStringGenerationOptions(32,
        CharacterGroup.Get(CharacterGroups.AllAlphaNumeric | CharacterGroups.Minus | CharacterGroups.Underscore));

    /// <summary>
    /// Initializes a new instance of the <see cref="RandomStringGenerationOptions"/> class.
    /// </summary>
    /// <param name="desiredStringLength">The desired length of the generated string.</param>
    /// <param name="allowedCharacters">The set of allowed characters used for string generation.</param>
    /// <param name="eachCharacterMustOccurAtLeastOnce">Specifies whether each character in the <paramref name="allowedCharacters"/>
    /// array must occur at least once in the generated random string.
    /// </param>
    public RandomStringGenerationOptions(
      int desiredStringLength,
      char[] allowedCharacters,
      bool eachCharacterMustOccurAtLeastOnce = false)
    {
      this.StringLength = desiredStringLength;
      this.AllowedCharacters = allowedCharacters ?? throw new ArgumentNullException(nameof(allowedCharacters));
      this.EachCharacterMustOccurAtLeastOnce = eachCharacterMustOccurAtLeastOnce;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RandomStringGenerationOptions"/> class using default values.
    /// </summary>
    public RandomStringGenerationOptions()
    {
      this.StringLength = Default.StringLength;
      this.AllowedCharacters = Default.AllowedCharacters;
      this.EachCharacterMustOccurAtLeastOnce = Default.EachCharacterMustOccurAtLeastOnce;
    }

    /// <summary>
    /// Gets the desired length of the generated string.
    /// </summary>
    public int StringLength { get; set; }

    /// <summary>
    /// Gets the allowed characters used for string generation.
    /// </summary>
    public char[] AllowedCharacters { get; set; }

    /// <summary>
    /// Gets the flag specifying whether each character in the <see cref="AllowedCharacters"/>
    /// array must occur at least once in the generated random string.
    /// </summary>
    public bool EachCharacterMustOccurAtLeastOnce { get; set; }
  }
}
