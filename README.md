# Verticular.Extensions.RandomStrings

The intend of this package is to help with generating random strings.

For this reason the package should result in a nuget package which supports as much dotnet framework versions as possible.

[![Build Status](https://martinhudasch.visualstudio.com/Verticular.Extensions.RandomStrings/_apis/build/status/mhudasch.verticular.extensions.randomstrings)](https://martinhudasch.visualstudio.com/Verticular.Extensions.RandomStrings/_build/latest?definitionId=4)
[![SonarQuality](https://sonarcloud.io/api/project_badges/measure?project=mhudasch_verticular.extensions.randomstrings&metric=alert_status)](https://sonarcloud.io/dashboard?id=mhudasch_verticular.extensions.randomstrings)

[![SonarLOC](https://sonarcloud.io/api/project_badges/measure?project=mhudasch_verticular.extensions.randomstrings&metric=ncloc)](https://sonarcloud.io/dashboard?id=mhudasch_verticular.extensions.randomstrings)

[![SonarBugs](https://sonarcloud.io/api/project_badges/measure?project=mhudasch_verticular.extensions.randomstrings&metric=bugs)](https://sonarcloud.io/dashboard?id=mhudasch_verticular.extensions.randomstrings)
[![SonarCodeSmells](https://sonarcloud.io/api/project_badges/measure?project=mhudasch_verticular.extensions.randomstrings&metric=code_smells)](https://sonarcloud.io/dashboard?id=mhudasch_verticular.extensions.randomstrings)
[![SonarDuplicates](https://sonarcloud.io/api/project_badges/measure?project=mhudasch_verticular.extensions.randomstrings&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=mhudasch_verticular.extensions.randomstrings)
[![SonarDepth](https://sonarcloud.io/api/project_badges/measure?project=mhudasch_verticular.extensions.randomstrings&metric=sqale_index)](https://sonarcloud.io/dashboard?id=mhudasch_verticular.extensions.randomstrings)

[![SonarMaintain](https://sonarcloud.io/api/project_badges/measure?project=mhudasch_verticular.extensions.randomstrings&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=mhudasch_verticular.extensions.randomstrings)
[![SonarRely](https://sonarcloud.io/api/project_badges/measure?project=mhudasch_verticular.extensions.randomstrings&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=mhudasch_verticular.extensions.randomstrings)
[![SonarSec](https://sonarcloud.io/api/project_badges/measure?project=mhudasch_verticular.extensions.randomstrings&metric=security_rating)](https://sonarcloud.io/dashboard?id=mhudasch_verticular.extensions.randomstrings)

[![NuGet Pre Release](https://img.shields.io/nuget/vpre/Verticular.Extensions.RandomStrings.svg)](https://www.nuget.org/packages/verticular.extensions.randomstrings/)

Currently supported frameworks are:

- [x] net45, net451, net452
- [x] net46, net461, net462
- [x] net47, net471, net472
- [ ] net48, net50
- [x] netstandard2.0, netstandard2.1

The standard versions of the package can be used in any netcoreapp2.0+.

## Usage

There are two random string generators available to control the kind of randomness used.

### Pseudo Random

The pseudo random string generator uses the `System.Random` class as the underlying infrastructure for
randomness. Using this class random strings generation is fast but less secure. That is why this generator
shouldn't be used for password generation.

### Cryptographic Random

As the name suggests this random string generator is more secure because it uses the `System.Security.Cryptography.RNGCryptoServiceProvider`
class as the underlying infrastructure for randomness. As Microsoft advises this provider is slower
but more secure to provide randomness as `System.Random`.

### Code Samples

#### Static Access

For quick in-place string generation the fastest way is to use the static `RandomStrings` class.

```cs

// quickly generate a random string with defaults
// the default is 32 characters login string containing
// all alpha-numeric characters and additionally '-', '_'.
var randomString = RandomString.PseudoRandom.Generate();

// the length can be specified with the corresponding parameter
var shorter = RandomString.PseudoRandom.Generate(length: 5);

// quickly controlling the allowed characters is achieved using the pre-defined
// character groups
// the example uses digits (0-9), only lower-case letters and the space-character
var usingGroups = RandomString.PseudoRandom.Generate(22,
  CharacterGroups.Digits | CharacterGroups.LowerCaseLetters | CharacterGroups.Space);

// for more fine grained control you can always pass an array of allowed characters
var usingArray = RandomString.PseudoRandom.Generate(12,
  new[]{ 'W', 'm', 'k', '2', 't' });

// for a more advanced scenario there is a builder available to control the
// options for generating random strings
// with the most amount of freedom this allows you to take a character group
// then combine that with manually additional characters and finally exclude some of them
var withBuilder = RandomString.PseudoRandom.Generate(builder => builder
  .WithLength(12)
  .AllowCharacters(CharacterGroups.AllAlphaNumeric)
  .AndAllowCharacters('<', '>')
  .ExcludeCharacters('Q', 'M'));

```

Of cause all the above code also works with the cryptographic variant.

```cs

// just use the crypto property
var generatedPassword = RandomString.CryptographicRandom.Generate(builder => builder
  .WithLength(25)
  .AllowCharacters(CharacterGroups.Letters | CharacterGroups.Digits)
  .ExcludeSimilarLookingCharacters()
  .EachCharacterMustOccurAtLeastOnce());

```

The previous call showcases two other options.

- `ExcludeSimilarLookingCharacters` is handy when you have to exclude characters
  like 'l' (lower-case L), '1' (one), '|' (pipe), 'I' (upper-case i) and others
  to improves string readability with difficult font environments.

- `EachCharacterMustOccurAtLeastOnce` pretty much explains itself. This option
  can for example be used to scramble a given text.

#### Access using dependency injection

Maybe it is necessary to use multiple random string generators or mock the generation. In those
cases you could use the following approach.

```cs

// during dependency injection setup
...
services.AddTransient<IRandomStringGenerator, CryptographicRandomStringGenerator>();
...

// and than later use it
public class SomeClass
{
  public SomeClass(IRandomStringGenerator generator)
  {
    var password = RandomString.PseudoRandom.Generate(builder => builder
      .WithLength(12)
      .AllowCharacters(CharacterGroups.AllReadableAsciiLetters)
      .ExcludeSimilarLookingCharacters());
  }
}

```

Another example would be to even inject the options for random string generation.

```cs

// during dependency injection setup
...
services.Configure<RandomStringGenerationOptions>(c =>
  {
    c.StringLength = 23;
    c.AllowCharacters = CharacterGroups.AllAlphaNumeric.ToCharArray();
  });
services.AddTransient<IRandomStringGenerator, CryptographicRandomStringGenerator>();
...

// and than later use it
public class SomeClass
{
  public SomeClass(IRandomStringGenerator generator,
    IOptions<RandomStringGenerationOptions> passwordRules)
  {
    var password = RandomString.CryptographicRandom.Generate(passwordRules.Value);
  }
}

```
