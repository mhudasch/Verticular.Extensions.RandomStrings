namespace System
{
  using System.Collections.Generic;
  using System.Linq;
  using Verticular.Extensions.RandomStrings;

  /// <summary>
  /// The base class for generating a random string.
  /// </summary>
  public abstract class RandomStringGeneratorBase : IRandomStringGenerator
  {
    private readonly IRandomNumberGenerator randomNumberGenerator;

    internal RandomStringGeneratorBase(IRandomNumberGenerator randomNumberGenerator)
    {
      this.randomNumberGenerator = randomNumberGenerator ?? throw new ArgumentNullException(nameof(randomNumberGenerator));
    }

    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="length">The desired length of the generated string.</param>
    /// <param name="allowedCharacters">The set of allowed characters used for string generation.</param>
    /// <param name="eachCharacterMustOccurAtLeastOnce">Specifies whether each character in the <paramref name="allowedCharacters"/>
    /// array must occur at least once in the generated random string.</param>
    /// <returns>The generated string.</returns>
    protected virtual string CoreCreate(int length, CharacterGroups allowedCharacters, bool eachCharacterMustOccurAtLeastOnce) =>
      this.CoreCreate(length, CharacterGroup.Get(allowedCharacters), eachCharacterMustOccurAtLeastOnce);

    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="length">The desired length of the generated string.</param>
    /// <param name="allowedCharacters">The set of allowed characters used for string generation.</param>
    /// <param name="eachCharacterMustOccurAtLeastOnce">Specifies whether each character in the <paramref name="allowedCharacters"/>
    /// array must occur at least once in the generated random string.</param>
    /// <returns>The generated string.</returns>
    protected virtual string CoreCreate(int length, char[] allowedCharacters, bool eachCharacterMustOccurAtLeastOnce)
    {
      if (length <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(length), "The length of the random string must be a positive non-zero integer.");
      }

      if (allowedCharacters.Length <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(allowedCharacters), "There must be at least one allowed character to create a random string.");
      }

      var result = new char[length];
      if (eachCharacterMustOccurAtLeastOnce)
      {
        if (allowedCharacters.Length > length)
        {
          throw new InvalidOperationException("When the flag for 'each character must occur at least once' is used the desired length of the " +
            $"random string must be at least as long as the number of allowed characters (requested length: {length} - minimum required length: {allowedCharacters.Length}).");
        }

        var randomizedIndizes = Enumerable.Range(0, length).OrderBy(_ => this.randomNumberGenerator.GetNextRandomNumber(length)).ToArray();
        for (var i = 0; i < length; i++)
        {
          result[randomizedIndizes[i]] = allowedCharacters[i % allowedCharacters.Length];
        }
      }
      else
      {
        for (var i = 0; i < length; i++)
        {
          result[i] = allowedCharacters[this.randomNumberGenerator.GetNextRandomNumber(allowedCharacters.Length)];
        }
      }
      return new string(result);
    }

    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="length">The desired length of the generated string.</param>
    /// <param name="allowedCharacters">The set of allowed characters used for string generation.</param>
    /// <param name="eachCharacterMustOccurAtLeastOnce">Specifies whether each character in the <paramref name="allowedCharacters"/>
    /// array must occur at least once in the generated random string.</param>
    /// <returns>The generated string.</returns>
    public virtual string Generate(int length, char[] allowedCharacters, bool eachCharacterMustOccurAtLeastOnce = false) =>
      this.CoreCreate(length, allowedCharacters, eachCharacterMustOccurAtLeastOnce);

    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="length">The desired length of the generated string.</param>
    /// <param name="allowedCharacters">The set of allowed characters used for string generation.</param>
    /// <param name="eachCharacterMustOccurAtLeastOnce">Specifies whether each character in the <paramref name="allowedCharacters"/>
    /// array must occur at least once in the generated random string.</param>
    /// <returns>The generated string.</returns>
    public virtual string Generate(int length, CharacterGroups allowedCharacters, bool eachCharacterMustOccurAtLeastOnce = false) =>
      this.CoreCreate(length, allowedCharacters, eachCharacterMustOccurAtLeastOnce);

    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="length">The desired length of the generated string.</param>
    /// <returns>The generated string.</returns>
    public virtual string Generate(int length) =>
      this.Generate(length, RandomStringGenerationOptions.Default.AllowedCharacters);

    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="builder">The builder that defines the options for generating a random string.</param>
    /// <returns>The generated string.</returns>
    public virtual string Generate(Action<IRandomStringGenerationBuilder> builder)
    {
      if (builder is null)
      {
        throw new ArgumentNullException(nameof(builder));
      }

      var built = new RandomStringGenerationBuilder();
      builder(built);
      return this.Generate(built.Build());
    }

    /// <summary>
    /// Generates a random string based on specified options.
    /// </summary>
    /// <param name="options">The options for generating a random string.</param>
    /// <returns>The generated string.</returns>
    public virtual string Generate(RandomStringGenerationOptions options)
    {
      if (options is null)
      {
        throw new ArgumentNullException(nameof(options));
      }

      return this.CoreCreate(options.StringLength, options.AllowedCharacters, options.EachCharacterMustOccurAtLeastOnce);
    }

    /// <summary>
    /// Generates a string using the default options from <see cref="RandomStringGenerationOptions.Default"/>.
    /// </summary>
    /// <returns>The generated string.</returns>
    public virtual string Generate() => this.Generate(RandomStringGenerationOptions.Default);
  }
}
