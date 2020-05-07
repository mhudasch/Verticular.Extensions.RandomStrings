namespace Verticular.Extensions.RandomStrings
{
  using System;
  using System.Security.Cryptography;

  internal class CryptographicRandomNumberGenerator : IRandomNumberGenerator
  {
    private readonly RandomNumberGenerator random;

    public CryptographicRandomNumberGenerator()
    {
      this.random = new RNGCryptoServiceProvider();
    }

    public int GetNextRandomNumber(int maxValue)
    {
      var buffer = new byte[sizeof(int)];
      this.random.GetBytes(buffer);
      var seed = BitConverter.ToInt32(buffer, 0);

      return Math.Abs(new Random(seed).Next(maxValue));
    }
  }
}
