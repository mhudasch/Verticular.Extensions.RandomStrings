namespace System
{
  using Verticular.Extensions.RandomStrings;

  /// <summary>
  /// This helper class can be used to create random strings.
  /// </summary>
  public static class RandomString
  {
    private static IRandomStringGenerator? pseudoRandomStringGenerator;

    /// <summary>
    /// Gets a singleton instance that uses the <see cref="Random"/> class to create a random string.
    /// This is a fast generator but should not be used to generate passwords.
    /// Use <see cref="CryptographicRandom"/> for that purpose.
    /// </summary>
    public static IRandomStringGenerator PseudoRandom
    {
      get
      {
        if (pseudoRandomStringGenerator == null) { pseudoRandomStringGenerator = new PseudoRandomStringGenerator(); }
        return pseudoRandomStringGenerator;
      }
    }

    private static IRandomStringGenerator? cryptographicRandomStringGenerator;

    /// <summary>
    /// Gets a singleton instance that uses the <see cref="System.Security.Cryptography.RNGCryptoServiceProvider"/> to create a random string.
    /// This is a slower generator but can be used for generating passwords.
    /// </summary>
    public static IRandomStringGenerator CryptographicRandom
    {
      get
      {
        if (cryptographicRandomStringGenerator == null) { cryptographicRandomStringGenerator = new CryptographicRandomStringGenerator(); }
        return cryptographicRandomStringGenerator;
      }
    }
  }
}
