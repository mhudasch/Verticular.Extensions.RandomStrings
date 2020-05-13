namespace System
{
  using System.Linq;
  using Verticular.Extensions.RandomStrings;

  /// <summary>
  /// The base class for generating a random string.
  /// </summary>
  public abstract class RandomStringGeneratorBase : IRandomStringGenerator
  {
    private readonly Func<IRandomNumberGenerator> randomNumberGeneratorFactory;
    private const int MaxLength = 5000;

    internal RandomStringGeneratorBase(Func<IRandomNumberGenerator> randomNumberGeneratorFactory)
    {
      this.randomNumberGeneratorFactory = randomNumberGeneratorFactory
        ?? throw new ArgumentNullException(nameof(randomNumberGeneratorFactory));
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

      if(length > MaxLength)
      {
        throw new ArgumentOutOfRangeException(nameof(length), $"To prevent memory issues the maximum length for random strings is {MaxLength}.");
      }

      if (allowedCharacters is null)
      {
        throw new ArgumentNullException(nameof(allowedCharacters));
      }

      if (allowedCharacters.Length <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(allowedCharacters), "There must be at least one allowed character to create a random string.");
      }

      if (allowedCharacters.Length > MaxLength)
      {
        throw new ArgumentOutOfRangeException(nameof(allowedCharacters), $"To prevent memory issues the maximum size of the allowed characters array is {MaxLength}.");
      }

      // ensure that every generate call has its own new random number generator instance
      var randomNumberGenerator = this.randomNumberGeneratorFactory();

      var result = new char[length];

      if (eachCharacterMustOccurAtLeastOnce)
      {
        if (allowedCharacters.Length > length)
        {
          throw new InvalidOperationException("When the flag for 'each character must occur at least once' is used the desired length of the " +
            $"random string must be at least as long as the number of allowed characters (requested length: {length} - minimum required length: {allowedCharacters.Length}).");
        }

        // shuffle the indizes of the target array so the placing is random when we have to
        // use all allowed characters
        var randomizedIndizes = Enumerable.Range(0, length).OrderBy(_ => randomNumberGenerator.GetNextRandomNumber(length)).ToArray();
        for (var i = 0; i < length; i++)
        {
          // use all allowed characters once an place them at a rantom index
          // in the target array
          if (i < allowedCharacters.Length)
          {
            result[randomizedIndizes[i]] = allowedCharacters[i];
          }
          else
          {
            // when all allowed characters are places once randomize both the index and the used character
            result[randomizedIndizes[i]] = allowedCharacters[randomNumberGenerator.GetNextRandomNumber(allowedCharacters.Length)];
          }
        }
      }
      else
      {
        for (var i = 0; i < length; i++)
        {
          result[i] = allowedCharacters[randomNumberGenerator.GetNextRandomNumber(allowedCharacters.Length)];
        }
      }

      if (randomNumberGenerator is IDisposable disposable)
      {
        disposable.Dispose();
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
