namespace Verticular.Extensions.RandomStrings
{
  using System;

  internal static class CharacterGroups
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

    public static char[] Get(CharacterGroup allowedCharacters)
    {
      if (allowedCharacters == CharacterGroup.None)
      {
        throw new ArgumentOutOfRangeException(nameof(allowedCharacters),
          "At least one character group is needed to generate a random string.");
      }

      var allowedCharactersArrayLength = 0;
      if (allowedCharacters.HasFlag(CharacterGroup.UpperCaseLetters))
      {
        allowedCharactersArrayLength += CharacterGroups.UpperCaseLetters.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroup.LowerCaseLetters))
      {
        allowedCharactersArrayLength += CharacterGroups.LowerCaseLetters.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroup.Digits))
      {
        allowedCharactersArrayLength += CharacterGroups.Digits.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroup.Minus))
      {
        allowedCharactersArrayLength += CharacterGroups.Minus.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroup.Underscore))
      {
        allowedCharactersArrayLength += CharacterGroups.Underscore.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroup.Space))
      {
        allowedCharactersArrayLength += CharacterGroups.Space.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroup.Brackets))
      {
        allowedCharactersArrayLength += CharacterGroups.Bracket.Length;
      }

      if (allowedCharacters.HasFlag(CharacterGroup.SpecialReadableAsciiLetters))
      {
        allowedCharactersArrayLength += CharacterGroups.SpecialReadableAscriiLetters.Length;
      }

      var finalArray = new char[allowedCharactersArrayLength];
      var offset = 0;
      const int itemSize = sizeof(char);
      if (allowedCharacters.HasFlag(CharacterGroup.UpperCaseLetters))
      {
        Buffer.BlockCopy(CharacterGroups.UpperCaseLetters, 0, finalArray, offset, CharacterGroups.UpperCaseLetters.Length * itemSize);
        offset += (CharacterGroups.UpperCaseLetters.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroup.LowerCaseLetters))
      {
        Buffer.BlockCopy(CharacterGroups.LowerCaseLetters, 0, finalArray, offset, CharacterGroups.LowerCaseLetters.Length * itemSize);
        offset += (CharacterGroups.LowerCaseLetters.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroup.Digits))
      {
        Buffer.BlockCopy(CharacterGroups.Digits, 0, finalArray, offset, CharacterGroups.Digits.Length * itemSize);
        offset += (CharacterGroups.Digits.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroup.Minus))
      {
        Buffer.BlockCopy(CharacterGroups.Minus, 0, finalArray, offset, CharacterGroups.Minus.Length * itemSize);
        offset += (CharacterGroups.Minus.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroup.Underscore))
      {
        Buffer.BlockCopy(CharacterGroups.Underscore, 0, finalArray, offset, CharacterGroups.Underscore.Length * itemSize);
        offset += (CharacterGroups.Underscore.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroup.Space))
      {
        Buffer.BlockCopy(CharacterGroups.Space, 0, finalArray, offset, CharacterGroups.Space.Length * itemSize);
        offset += (CharacterGroups.Space.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroup.Brackets))
      {
        Buffer.BlockCopy(CharacterGroups.Bracket, 0, finalArray, offset, CharacterGroups.Bracket.Length * itemSize);
        offset += (CharacterGroups.Bracket.Length * sizeof(char));
      }

      if (allowedCharacters.HasFlag(CharacterGroup.SpecialReadableAsciiLetters))
      {
        Buffer.BlockCopy(CharacterGroups.SpecialReadableAscriiLetters, 0, finalArray,
          offset, CharacterGroups.SpecialReadableAscriiLetters.Length * itemSize);
        offset += (CharacterGroups.SpecialReadableAscriiLetters.Length * sizeof(char));
      }

      return finalArray;
    }
  }
}
