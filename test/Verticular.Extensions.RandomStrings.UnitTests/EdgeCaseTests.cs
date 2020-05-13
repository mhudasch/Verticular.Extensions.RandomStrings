namespace Verticular.Extensions.RandomStrings.UnitTests
{
  using System;
  using System.Collections.Generic;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  [TestClass]
  public class EdgeCaseTests
  {
    private static readonly char[] Empty =
#if !(NET45 || NET451 || NET452)
      Array.Empty<char>();
#else
      new char[]{};
#endif

    [TestMethod]
    public void NullBuilderTest()
    {
      // assert
      Assert.ThrowsException<ArgumentNullException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(builder: (Action<IRandomStringGenerationBuilder>)null);
      });
    }

    [TestMethod]
    public void NullOptionTest()
    {
      // assert
      Assert.ThrowsException<ArgumentNullException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(options: (RandomStringGenerationOptions)null);
      });
    }

    [TestMethod]
    public void BuilderAllowedCharactersEachTest()
    {
      // assert
      Assert.ThrowsException<InvalidOperationException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(builder => builder
          .WithLength(12)
          .AllowCharacters(CharacterGroups.AllAlphaNumeric)
          .EachCharacterMustOccurAtLeastOnce());
      });
    }

    [TestMethod]
    public void BuilderAllowedCharactersNoneTest()
    {
      // assert
      Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(builder => builder
          .WithLength(12)
          .AllowCharacters(CharacterGroups.None));
      });
    }

    [TestMethod]
    public void BuilderAndAllowCharactersNull()
    {
      // assert
      Assert.ThrowsException<ArgumentNullException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(builder => builder
          .WithLength(10)
          .AllowCharacters(CharacterGroups.Letters)
          .AndAllowCharacters((char[])null));
      });
    }

    [TestMethod]
    public void BuilderAndAllowCharactersNone()
    {
      // assert
      Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(builder => builder
          .WithLength(10)
          .AllowCharacters(CharacterGroups.Letters)
          .AndAllowCharacters(CharacterGroups.None));
      });
    }

    [TestMethod]
    public void GeneratorAllowedCharactersNoneTest()
    {
      // assert
      Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(12, CharacterGroups.None);
      });
    }

    [TestMethod]
    public void BuilderNullAllowedCharactersTest()
    {
      // assert
      Assert.ThrowsException<ArgumentNullException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(builder => builder
          .WithLength(12)
          .AllowCharacters((char[])null));
      });
    }

    [TestMethod]
    public void BuilderAndExcludeCharactersNull()
    {
      // assert
      Assert.ThrowsException<ArgumentNullException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(builder => builder
          .WithLength(10)
          .AllowCharacters(CharacterGroups.Letters)
          .ExcludeCharacters((char[])null));
      });
    }

    [TestMethod]
    public void GeneratorAllowedCharactersLengthEachTest()
    {
      // assert
      Assert.ThrowsException<InvalidOperationException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(12, CharacterGroups.AllAlphaNumeric, true);
      });
    }

    [TestMethod]
    public void BuilderAllowedCharactersLengthEachTest()
    {
      // assert
      Assert.ThrowsException<InvalidOperationException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(builder => builder
          .WithLength(12)
          .AllowCharacters(CharacterGroups.AllAlphaNumeric)
          .EachCharacterMustOccurAtLeastOnce());
      });
    }

    [DataTestMethod]
    [DynamicData(nameof(LengthEdges), DynamicDataSourceType.Method)]
    public void GeneratorPseudoRandomStringLengthTests(int length)
    {
      // assert
      Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(length, CharacterGroups.AllAlphaNumeric);
      });
    }

    [DataTestMethod]
    [DynamicData(nameof(LengthEdges), DynamicDataSourceType.Method)]
    public void BuilderPseudoRandomStringLengthTests(int length)
    {
      // assert
      Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
      {
        var _ = RandomString.PseudoRandom.Generate(builder => builder.WithLength(length));
      });
    }

    [DataTestMethod]
    [DynamicData(nameof(CharacterArrayEdges), DynamicDataSourceType.Method)]
    public void PseudoRandomStringAllowedCharactersArrayTests(char[] allowedCharacters)
    {
      // assert
      if (allowedCharacters is null)
      {
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
          var _ = RandomString.PseudoRandom.Generate(50, allowedCharacters);
        });
      }
      else
      {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
          var _ = RandomString.PseudoRandom.Generate(50, allowedCharacters);
        });
      }
    }

    [TestMethod]
    public void OptionsZeroLengthTest()
    {
      // arrange
      var options = new RandomStringGenerationOptions(0, CharacterGroups.AllAlphaNumeric.ToCharArray(), true);

      // assert
      Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
      {
        // act
        var _ = RandomString.PseudoRandom.Generate(options);
      });
    }

    [TestMethod]
    public void OptionsNullAllowedTest()
    {
      // arrange

      // assert
      Assert.ThrowsException<ArgumentNullException>(() =>
      {
        // act
        var _ = new RandomStringGenerationOptions(0, null, true);
      });
    }


    public static IEnumerable<object[]> LengthEdges()
    {
      yield return new object[] { int.MaxValue };
      yield return new object[] { 100000 };
      yield return new object[] { 0 };
      yield return new object[] { -100 };
      yield return new object[] { int.MinValue };
    }

    public static IEnumerable<object[]> CharacterArrayEdges()
    {
      yield return new object[] { Empty };
      yield return new object[] { null };
      yield return new object[] { new char[50000] };
    }
  }
}
