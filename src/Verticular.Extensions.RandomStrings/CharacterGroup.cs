namespace System
{
  /// <summary>
  /// A shortcut for selecting specific groups of allowed characters.
  /// </summary>
  [Flags]
  public enum CharacterGroup
  {
    /// <summary>
    /// No character
    /// </summary>
    None = 0,
    /// <summary>
    /// Latin upper-case characters A,B,C ...
    /// </summary>
    UpperCaseLetters = 1,
    /// <summary>
    /// Latin lower-case characters a,b,c ...
    /// </summary>
    LowerCaseLetters = 2,
    /// <summary>
    /// All latin upper-case and lower-case letters
    /// </summary>
    Letters = UpperCaseLetters | LowerCaseLetters,
    /// <summary>
    /// Arabic digits 1,2,3 ...
    /// </summary>
    Digits = 4,
    /// <summary>
    /// All latin upper-case, lower-case letters and digits from 0 to 9.
    /// </summary>
    AllAlphaNumeric = Letters | Digits,
    /// <summary>
    /// The minus ('-') character
    /// </summary>
    Minus = 8,
    /// <summary>
    /// The underscore ('_') character 
    /// </summary>
    Underscore = 16,
    /// <summary>
    /// The space (' ') character
    /// </summary>
    Space = 32,
    /// <summary>
    /// The characters usable to create names for files or directories.
    /// </summary>
    FileSystemSafe = AllAlphaNumeric | Minus | Underscore,
    /// <summary>
    /// All bracket characters '&lt;', '&gt;', '{', '}', '[', ']', '(', ')'
    /// </summary>
    Brackets = 64,
    /// <summary>
    /// All readable special ascii characters - '!', '&quot;', '#', '$', '%', '&amp;', ''', '*', '+', ',', '.', '/', ':', ';', '=', '?', '@', '\', '^', '´', '`', '|', '~'
    /// </summary>
    SpecialReadableAsciiLetters = 128,
    /// <summary>
    /// All readable ascii characters (code 32 till code 126)
    /// </summary>
    AllReadableAsciiLetters = AllAlphaNumeric | Minus | Underscore | Space | Brackets | SpecialReadableAsciiLetters
  }
}
