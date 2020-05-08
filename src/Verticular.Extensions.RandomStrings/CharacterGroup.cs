namespace Verticular.Extensions.RandomStrings
{
  using System;

  internal static class CharacterGroup
  {
    public static readonly char[] UpperCaseLetters =
    {
      'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
      'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
      'Q', 'R', 'S', 'T', 'U', 'W', 'X', 'Y', 'Z'
    };

    public static readonly char[] LowerCaseLetters =
    {
      'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
      'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
      'q', 'r', 's', 't', 'u', 'w', 'x', 'y', 'z'
    };

    public static readonly char[] Digits =
    {
      '1','2','3','4','5','6','7','8','9','0'
    };

    public static readonly char[] Minus = { '-' };
    public static readonly char[] Underscore = { '_' };
    public static readonly char[] Space = { ' ' };
    public static readonly char[] Bracket = { '<', '>', '{', '}', '[', ']', '(', ')' };
    public static readonly char[] SpecialReadableAscriiLetters = { '!', '"', '#', '$', '%', '&', '\'', '*', '+', ',', '.', '/', ':', ';', '=', '?', '@', '\\', '^', '´', '`', '|', '~' };

    public static readonly char[] SimilarLookingCharacters = { '1', 'l', 'I', '|', 'o', 'O', '0' };

    public static char[] Get(CharacterGroups allowedCharacters)
    {
      if (allowedCharacters == CharacterGroups.None)
      {
        throw new ArgumentOutOfRangeException(nameof(allowedCharacters),
          "At least one character group is needed to generate a random string.");
      }

      var allowedCharactersArrayLength = 0;
      if (allowedCharacters.HasFlag(CharacterGroups.UpperCaseLetters))
      {
        allowedCharactersArrayLength += CharacterGroup.UpperCaseLetters.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroups.LowerCaseLetters))
      {
        allowedCharactersArrayLength += CharacterGroup.LowerCaseLetters.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroups.Digits))
      {
        allowedCharactersArrayLength += CharacterGroup.Digits.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroups.Minus))
      {
        allowedCharactersArrayLength += CharacterGroup.Minus.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroups.Underscore))
      {
        allowedCharactersArrayLength += CharacterGroup.Underscore.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroups.Space))
      {
        allowedCharactersArrayLength += CharacterGroup.Space.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroups.Brackets))
      {
        allowedCharactersArrayLength += CharacterGroup.Bracket.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroups.SpecialReadableAsciiLetters))
      {
        allowedCharactersArrayLength += CharacterGroup.SpecialReadableAscriiLetters.Length;
      }

      var finalArray = new char[allowedCharactersArrayLength];
      var offset = 0;
      const int itemSize = sizeof(char);
      if (allowedCharacters.HasFlag(CharacterGroups.UpperCaseLetters))
      {
        Buffer.BlockCopy(CharacterGroup.UpperCaseLetters, 0, finalArray, offset, CharacterGroup.UpperCaseLetters.Length * itemSize);
        offset += (CharacterGroup.UpperCaseLetters.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroups.LowerCaseLetters))
      {
        Buffer.BlockCopy(CharacterGroup.LowerCaseLetters, 0, finalArray, offset, CharacterGroup.LowerCaseLetters.Length * itemSize);
        offset += (CharacterGroup.LowerCaseLetters.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroups.Digits))
      {
        Buffer.BlockCopy(CharacterGroup.Digits, 0, finalArray, offset, CharacterGroup.Digits.Length * itemSize);
        offset += (CharacterGroup.Digits.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroups.Minus))
      {
        Buffer.BlockCopy(CharacterGroup.Minus, 0, finalArray, offset, CharacterGroup.Minus.Length * itemSize);
        offset += (CharacterGroup.Minus.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroups.Underscore))
      {
        Buffer.BlockCopy(CharacterGroup.Underscore, 0, finalArray, offset, CharacterGroup.Underscore.Length * itemSize);
        offset += (CharacterGroup.Underscore.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroups.Space))
      {
        Buffer.BlockCopy(CharacterGroup.Space, 0, finalArray, offset, CharacterGroup.Space.Length * itemSize);
        offset += (CharacterGroup.Space.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroups.Brackets))
      {
        Buffer.BlockCopy(CharacterGroup.Bracket, 0, finalArray, offset, CharacterGroup.Bracket.Length * itemSize);
        offset += (CharacterGroup.Bracket.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroups.SpecialReadableAsciiLetters))
      {
        Buffer.BlockCopy(CharacterGroup.SpecialReadableAscriiLetters, 0, finalArray,
          offset, CharacterGroup.SpecialReadableAscriiLetters.Length * itemSize);
        // do not forget offset increment when adding new character groups after this!
      }

      return finalArray;
    }
  }
}
