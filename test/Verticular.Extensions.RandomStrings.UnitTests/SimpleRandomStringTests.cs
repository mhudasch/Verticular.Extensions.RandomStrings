namespace Verticular.Extensions.RandomStrings.UnitTests
{
  using System;
  using System.Text.RegularExpressions;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  [TestClass]
  public class SimpleRandomStringTests
  {
    [TestMethod]
    public void PseudoRandomTest()
    {
      // arrange
      // act
      var random = RandomString.PseudoRandom.Generate();

      // assert
      Assert.IsNotNull(random);
      StringAssert.Matches(random, new Regex("^[A-Za-z0-9_\\-]{32}$"));
    }

    [TestMethod]
    public void CryptoRandomTest()
    {
      // arrange
      // act
      var random = RandomString.CryptographicRandom.Generate();

      // assert
      Assert.IsNotNull(random);
      StringAssert.Matches(random, new Regex("^[A-Za-z0-9_\\-]{32}$"));
    }


  }
}
