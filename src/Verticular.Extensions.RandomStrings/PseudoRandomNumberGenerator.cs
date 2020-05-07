namespace Verticular.Extensions.RandomStrings
{
  using System;

  internal class PseudoRandomNumberGenerator : IRandomNumberGenerator
  {
    private readonly Random random;

    public PseudoRandomNumberGenerator()
    {
      this.random = new Random();
    }

    public int GetNextRandomNumber(int maxValue)
    {
      return (int)this.random.Next(maxValue);
    }
  }
}
