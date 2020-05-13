namespace Verticular.Extensions.RandomStrings.UnitTests
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  [TestClass]
  public class SimpleRandomStringTests
  {
    private static readonly char[] Empty =
#if !(NET45 || NET451 || NET452)
      Array.Empty<char>();
#else
      new char[]{};
#endif

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

    [TestMethod]
    public void SimpleLengthTest()
    {
      // arrange
      var allowed = RandomStringGenerationOptions.Default.AllowedCharacters;

      // act
      var random = RandomString.CryptographicRandom.Generate(10);

      // assert
      Assert.IsNotNull(random);
      Assert.AreEqual(10, random.Length);
      Assert.IsTrue(random.All(c => allowed.Contains(c)));
    }

    [TestMethod]
    public void SimpleBuilderTest()
    {
      // arrange
      var allowed = CharacterGroups.Letters.ToCharArray().Concat(CharacterGroups.Digits.ToCharArray()).ToArray();

      // act
      var random = RandomString.PseudoRandom.Generate(builder => builder
        .WithLength(10)
        .AllowCharacters(CharacterGroups.Letters)
        .AndAllowCharacters(CharacterGroups.Digits));

      // assert
      Assert.IsNotNull(random);
      Assert.AreEqual(10, random.Length);
      Assert.IsTrue(random.All(c => allowed.Contains(c)));
    }

    [TestMethod]
    public void SimpleOptionsTest()
    {
      // arrange
      var length = RandomStringGenerationOptions.Default.StringLength;
      var allowed = RandomStringGenerationOptions.Default.AllowedCharacters;

      // act
      var random = RandomString.PseudoRandom.Generate(new RandomStringGenerationOptions());

      // assert
      Assert.IsNotNull(random);
      Assert.AreEqual(length, random.Length);
      Assert.IsTrue(random.All(c => allowed.Contains(c)));
    }

    [TestMethod]
    public void SimpleOptionsTest2()
    {
      // arrange
      var options = new RandomStringGenerationOptions(200, CharacterGroups.AllAlphaNumeric.ToCharArray(), true);

      // act
      var random = RandomString.PseudoRandom.Generate(options);

      // assert
      Assert.IsNotNull(random);
      Assert.AreEqual(options.StringLength, random.Length);
      Assert.IsTrue(random.All(c => options.AllowedCharacters.Contains(c)));
      Assert.IsTrue(options.AllowedCharacters.All(c => random.Contains(c)));
    }

    [TestMethod]
    public void SimpleBuilderNoSimilarTest()
    {
      // arrange
      var length = 2000;
      var allowed = CharacterGroups.AllAlphaNumeric.ToCharArray().Except(new[] { '1', 'l', 'I', '|', 'o', 'O', '0' }).ToArray();

      // act
      var random = RandomString.PseudoRandom.Generate(builder => builder
        .WithLength(length)
        .AllowCharacters(CharacterGroups.AllAlphaNumeric)
        .ExcludeSimilarLookingCharacters());

      // assert
      Assert.IsNotNull(random);
      Assert.AreEqual(length, random.Length);
      Assert.IsTrue(random.All(c => allowed.Contains(c)));
    }

    [DataTestMethod]
    [DynamicData(nameof(PseudoRandomBuilderArgumentsFromGroups), DynamicDataSourceType.Method)]
    public void PseudoRandomBuilderFromGroupsTests(int length, CharacterGroups allowedCharacterGroup,
      char[] additionalCharacters, char[] exclusions)
    {
      // arrange
      var allowed = allowedCharacterGroup.ToCharArray().Concat(additionalCharacters).Except(exclusions).ToArray();

      // act
      var random = RandomString.PseudoRandom.Generate(builder =>
      {
        builder.WithLength(length)
        .AllowCharacters(allowedCharacterGroup);

        if (additionalCharacters.Any())
        {
          builder.AndAllowCharacters(additionalCharacters);
        }

        if (exclusions.Any())
        {
          builder.ExcludeCharacters(exclusions);
        }

      });

      // assert
      Assert.IsNotNull(random);
      Assert.AreEqual(length, random.Length);
      Assert.IsTrue(random.All(c => allowed.Contains(c)));
      Assert.IsTrue(exclusions.All(c => !random.Contains(c)));
    }

    [DataTestMethod]
    [DynamicData(nameof(PseudoRandomBuilderArgumentsFromArray), DynamicDataSourceType.Method)]
    public void PseudoRandomBuilderFromArrayTests(int length, char[] allowedCharacterGroup,
       CharacterGroups? additionalCharacters, char[] exclusions)
    {
      // arrange
      var allowed = allowedCharacterGroup
      .Concat(additionalCharacters.HasValue ? additionalCharacters.Value.ToCharArray() : Enumerable.Empty<char>())
      .Except(exclusions).ToArray();

      // act
      var random = RandomString.PseudoRandom.Generate(builder =>
      {
        builder.WithLength(length)
        .AllowCharacters(allowedCharacterGroup);

        if (additionalCharacters.HasValue)
        {
          builder.AndAllowCharacters(additionalCharacters.Value);
        }

        if (exclusions.Any())
        {
          builder.ExcludeCharacters(exclusions);
        }

      });

      // assert
      Assert.IsNotNull(random);
      Assert.AreEqual(length, random.Length);
      Assert.IsTrue(random.All(c => allowed.Contains(c)));
      Assert.IsTrue(exclusions.All(c => !random.Contains(c)));
    }

    public static IEnumerable<object[]> PseudoRandomBuilderArgumentsFromGroups()
    {
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.AllAlphaNumeric, Empty, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.AllReadableAsciiLetters, Empty, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.Brackets, Empty, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.Digits, Empty, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.FileSystemSafe, Empty, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.Letters, Empty, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.LowerCaseLetters, Empty, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.SpecialReadableAsciiLetters, Empty, Empty };

      yield return new object[] { new Random().Next(10, 50), CharacterGroups.Letters, new char[] { '1', '2', '3' }, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.Letters, Empty, new char[] { 'A', 'B', 'C' } };
    }

    public static IEnumerable<object[]> PseudoRandomBuilderArgumentsFromArray()
    {
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.AllAlphaNumeric.ToCharArray(), (CharacterGroups?)null, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.AllReadableAsciiLetters.ToCharArray(), (CharacterGroups?)null, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.Brackets.ToCharArray(), (CharacterGroups?)null, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.Digits.ToCharArray(), (CharacterGroups?)null, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.FileSystemSafe.ToCharArray(), (CharacterGroups?)null, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.Letters.ToCharArray(), (CharacterGroups?)null, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.LowerCaseLetters.ToCharArray(), (CharacterGroups?)null, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.SpecialReadableAsciiLetters.ToCharArray(), (CharacterGroups?)null, Empty };

      yield return new object[] { new Random().Next(10, 50), CharacterGroups.AllAlphaNumeric.ToCharArray(), CharacterGroups.Digits, Empty };
      yield return new object[] { new Random().Next(10, 50), CharacterGroups.Letters.ToCharArray(), (CharacterGroups?)null, new char[] { 'A', 'B', 'C' } };
    }
  }
}
