namespace Verticular.Extensions.RandomStrings
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  internal class RandomStringGenerationBuilder : IRandomStringGenerationBuilder
  {
    private int stringLength;
    private List<char> allowedCharacters;
    private bool eachCharacterMustOccurAtLeastOnce;
    private bool excludeSimilarLookingCharacters;

    public RandomStringGenerationBuilder()
    {
      this.allowedCharacters = new List<char>(RandomStringGenerationOptions.Default.AllowedCharacters);
      this.stringLength = RandomStringGenerationOptions.Default.StringLength;
    }

    public IRandomStringGenerationBuilder WithLength(int stringLength)
    {
      if (stringLength <= 0)
      {
        throw new ArgumentOutOfRangeException(nameof(stringLength),
          "The length of the random string must be a positive non-zero integer.");
      }

      this.stringLength = stringLength;
      return this;
    }

    public IRandomStringGenerationBuilder AllowCharacters(params char[] characters)
    {
      if (characters is null)
      {
        throw new ArgumentNullException(nameof(characters));
      }

      this.allowedCharacters = new List<char>(characters);
      return this;
    }

    public IRandomStringGenerationBuilder AllowCharacters(CharacterGroups characters)
    {
      this.allowedCharacters = new List<char>(CharacterGroup.Get(characters));
      return this;
    }

    public IRandomStringGenerationBuilder AndAllowCharacters(params char[] characters)
    {
      if (characters is null)
      {
        throw new ArgumentNullException(nameof(characters));
      }

      foreach (var c in characters)
      {
        this.allowedCharacters.Add(c);
      }
      return this;
    }

    public IRandomStringGenerationBuilder AndAllowCharacters(CharacterGroups characters)
    {
      foreach (var c in CharacterGroup.Get(characters))
      {
        this.allowedCharacters.Add(c);
      }
      return this;
    }

    public IRandomStringGenerationBuilder ExcludeCharacters(params char[] characters)
    {
      if (characters is null)
      {
        throw new ArgumentNullException(nameof(characters));
      }

      foreach (var c in characters)
      {
        this.allowedCharacters.Remove(c);
      }
      return this;
    }

    public IRandomStringGenerationBuilder EachCharacterMustOccurAtLeastOnce()
    {
      this.eachCharacterMustOccurAtLeastOnce = true;
      return this;
    }

    public IRandomStringGenerationBuilder ExcludeSimilarLookingCharacters()
    {
      this.excludeSimilarLookingCharacters = true;
      return this;
    }

    internal RandomStringGenerationOptions Build()
    {
      if (this.excludeSimilarLookingCharacters)
      {
        foreach (var c in CharacterGroup.SimilarLookingCharacters)
        {
          this.allowedCharacters.Remove(c);
        }
      }

      if (this.eachCharacterMustOccurAtLeastOnce
        && (this.allowedCharacters.Count > this.stringLength))
      {
        throw new InvalidOperationException("When the flag for 'each character must occur at least once' is used the desired length of the " +
          $"random string must be at least as long as the number of allowed characters (requested length: {this.stringLength} - minimum required length: {this.allowedCharacters.Count}).");
      }

      return new RandomStringGenerationOptions(this.stringLength,
        this.allowedCharacters.ToArray(),
        this.eachCharacterMustOccurAtLeastOnce);
    }
  }
}
