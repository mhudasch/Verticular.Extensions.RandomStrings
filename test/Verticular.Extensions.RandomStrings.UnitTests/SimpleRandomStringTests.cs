namespace Verticular.Extensions.RandomStrings.UnitTests
{
  using System;
  using System.Linq;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  [TestClass]
  public class SimpleRandomStringTests
  {
    [TestMethod]
    public void PseudoRandomTest()
    {
      // arrange
      var options = RandomStringGenerationOptions.Default;

      // act
      var random = RandomString.PseudoRandom.Generate();

      // assert
      Assert.IsNotNull(random);
      Assert.AreEqual(options.StringLength, random.Length);
      Assert.IsTrue(random.All(c => options.AllowedCharacters.Contains(c)));
    }

    [TestMethod]
    public void CryptoRandomTest()
    {
      // arrange
      var options = RandomStringGenerationOptions.Default;

      // act
      var random = RandomString.CryptographicRandom.Generate();

      // assert
      Assert.IsNotNull(random);
      Assert.AreEqual(options.StringLength, random.Length);
      Assert.IsTrue(random.All(c => options.AllowedCharacters.Contains(c)));
    }

    [TestMethod]
    public void CryptoRandomOnceOccurranceTest()
    {
      // arrange
      var allowed = CharacterGroups.FileSystemSafe.ToCharArray();

      // act
      var random = RandomString.CryptographicRandom.Generate(allowed.Length * 2, allowed, true);

      // assert
      Assert.IsNotNull(random);
      Assert.AreEqual(allowed.Length * 2, random.Length);
      Assert.IsTrue(allowed.All(c => random.Contains(c)));
    }
  }
}
