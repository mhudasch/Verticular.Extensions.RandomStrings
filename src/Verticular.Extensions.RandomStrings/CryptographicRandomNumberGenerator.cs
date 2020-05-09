namespace Verticular.Extensions.RandomStrings
{
  using System;
  using System.Security.Cryptography;

  internal class CryptographicRandomNumberGenerator : IRandomNumberGenerator
  {
    private readonly RandomNumberGenerator random;

    public CryptographicRandomNumberGenerator()
    {
      this.random = RandomNumberGenerator.Create();
    }

    public int GetNextRandomNumber(int maxValue)
    {
      var processed = false;
      var count = (int)Math.Ceiling(Math.Log(maxValue, 2) / 8.0);
      var max = (int)(Math.Pow(2, count * 8) / maxValue) * maxValue;
#if !(NET45 || NET451 || NET452)
      var offset = BitConverter.IsLittleEndian ? 0 : sizeof(uint) - count;
#endif
      var result = -1;
      while (!processed)
      {
        var uintBuffer = new byte[sizeof(uint)];
#if !(NET45 || NET451 || NET452)
        this.random.GetBytes(uintBuffer, offset, count);
        var num = BitConverter.ToUInt32(uintBuffer, 0);
        if (num < max)
        {
          result = (int)(num % (uint)maxValue);
          processed = true;
        }
#else
        /*
         * Note:
         * Since the even spread could not be fixed in older framework versions because we cannot fill the byte count the following will occur...
         * For example the maxValue would be 62 allowed characters.
         * 62 valid characters is equal to 5,9541963103868752088061235991756 bits (log(62) / log(2)), 
         * so it won't divide evenly on a 32 bit number (uint).
         * what consequences does this have? 
         * As a result, the random output won't be uniform. 
         * Characters which are lower in value will occur more likely (just by a small fraction, but still it happens).
         * To be more precise, the first 4 characters of a valid array are 0,00000144354999199840239435286 % more likely to occur.
         */
        this.random.GetBytes(uintBuffer);
        var num = BitConverter.ToUInt32(uintBuffer, 0);
        result = (int)(num % (uint)maxValue);
        processed = true;
#endif
      }

      return result;
    }
  }
}
