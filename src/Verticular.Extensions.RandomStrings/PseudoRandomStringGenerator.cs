namespace Verticular.Extensions.RandomStrings
{
  using System;

  /// <summary>
  /// A random string generator that uses the <see cref="Random"/> class to create a random string.
  /// This is a fast generator but should not be used to generate passwords.
  /// Use <see cref="CryptographicRandomStringGenerator"/> for that purpose.
  /// </summary>
  public class PseudoRandomStringGenerator : RandomStringGeneratorBase
  {
    /// <summary>
    /// Creates new instance of the <see cref="PseudoRandomStringGenerator"/> class.
    /// </summary>
    public PseudoRandomStringGenerator()
      : base(() => new PseudoRandomNumberGenerator())
    {
    }
  }
}
