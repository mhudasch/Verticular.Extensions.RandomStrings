namespace RandomStringSamples
{
  using System;

  internal class Program
  {
    private static void Main(string[] _)
    {
      Console.WriteLine(RandomString.PseudoRandom.Generate(12));
    }
  }
}
