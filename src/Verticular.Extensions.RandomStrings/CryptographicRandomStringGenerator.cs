namespace Verticular.Extensions.RandomStrings
{
  using System;

  internal class CryptographicRandomStringGenerator : RandomStringGeneratorBase
  {
    public CryptographicRandomStringGenerator()
      : base(new CryptographicRandomNumberGenerator())
    {
    }
  }
}
