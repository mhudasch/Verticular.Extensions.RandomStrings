namespace Verticular.Extensions.RandomStrings
{
  using System;

  internal class PseudoRandomStringGenerator : RandomStringGeneratorBase
  {
    public PseudoRandomStringGenerator()
      : base(new PseudoRandomNumberGenerator())
    {
    }
  }
}
