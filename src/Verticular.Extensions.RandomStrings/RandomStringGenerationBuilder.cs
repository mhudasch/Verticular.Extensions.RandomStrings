namespace Verticular.Extensions.RandomStrings
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  internal class RandomStringGenerationBuilder : IRandomStringGenerationBuilder
  {
    private int stringLength;
    private HashSet<char> allowedCharacters;
    private bool eachCharacterMustOccurAtLeastOnce;
    private bool excludeSimilarLookingCharacters;

    public RandomStringGenerationBuilder()
    {
      this.allowedCharacters = new HashSet<char>(RandomStringGenerationOptions.Default.AllowedCharacters);
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
      this.allowedCharacters = new HashSet<char>(characters);
      return this;
    }

    public IRandomStringGenerationBuilder AllowCharacters(CharacterGroup characters)
    {
      this.allowedCharacters = new HashSet<char>(CharacterGroups.Get(characters));
      return this;
    }

    public IRandomStringGenerationBuilder AndAllowCharacters(params char[] characters)
    {
      foreach (var c in characters)
      {
        this.allowedCharacters.Add(c);
      }
      return this;
    }

    public IRandomStringGenerationBuilder AndAllowCharacters(CharacterGroup characters)
    {
      foreach (var c in CharacterGroups.Get(characters))
      {
        this.allowedCharacters.Add(c);
      }
      return this;
    }

    public IRandomStringGenerationBuilder ExcludeCharacters(params char[] characters)
    {
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
        foreach (var c in CharacterGroups.SimilarLookingCharacters)
        {
          this.allowedCharacters.Remove(c);
        }
      }

      if (this.eachCharacterMustOccurAtLeastOnce
        && (this.allowedCharacters.Count > this.stringLength))
      {
        throw new InvalidOperationException("When the flag for 'each character must occur at least once' is used the desired length of the " +
          "random string must be at least as long as the number of allowed characters.");
      }

      return new RandomStringGenerationOptions(this.stringLength,
        this.allowedCharacters.ToArray(),
        this.eachCharacterMustOccurAtLeastOnce);

    }
  }
}
