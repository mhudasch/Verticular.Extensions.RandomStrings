using Verticular.Extensions.RandomStrings;

namespace System
{
  /// <summary>
  /// A shortcut for selecting specific groups of allowed characters.
  /// </summary>
  [Flags]
  public enum CharacterGroups
  {
    /// <summary>
    /// No character
    /// </summary>
    None = 0,
    /// <summary>
    /// Latin upper-case characters A,B,C ... (Count: 26)
    /// </summary>
    UpperCaseLetters = 1,
    /// <summary>
    /// Latin lower-case characters a,b,c ... (Count: 26)
    /// </summary>
    LowerCaseLetters = 2,
    /// <summary>
    /// All latin upper-case and lower-case letters (Count: 52)
    /// </summary>
    Letters = UpperCaseLetters | LowerCaseLetters,
    /// <summary>
    /// Arabic digits 0,1,2,3 ... (Count: 10)
    /// </summary>
    Digits = 4,
    /// <summary>
    /// All latin upper-case, lower-case letters and digits from 0 to 9. (Count: 62)
    /// </summary>
    AllAlphaNumeric = Letters | Digits,
    /// <summary>
    /// The minus ('-') character (Count: 1)
    /// </summary>
    Minus = 8,
    /// <summary>
    /// The underscore ('_') character (Count: 1)
    /// </summary>
    Underscore = 16,
    /// <summary>
    /// The space (' ') character (Count: 1)
    /// </summary>
    Space = 32,
    /// <summary>
    /// The characters usable to create names for files or directories (A-Z,a-z,0-9 and '-','_'). (Count: 64)
    /// </summary>
    FileSystemSafe = AllAlphaNumeric | Minus | Underscore,
    /// <summary>
    /// All bracket characters '&lt;', '&gt;', '{', '}', '[', ']', '(', ')' (Count: 8)
    /// </summary>
    Brackets = 64,
    /// <summary>
    /// All readable special ascii characters - '!', '&quot;', '#', '$', '%', '&amp;', ''', '*', '+', ',', '.', '/', ':', ';', '=', '?', '@', '\', '^', '´', '`', '|', '~' (Count: 23)
    /// </summary>
    SpecialReadableAsciiLetters = 128,
    /// <summary>
    /// All readable ascii characters (code 32 till code 126). (Count: 96)
    /// </summary>
    AllReadableAsciiLetters = AllAlphaNumeric | Minus | Underscore | Space | Brackets | SpecialReadableAsciiLetters
  }
}
