using System;
using Newtonsoft.Json;

namespace RandomStringSamples
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine(JsonConvert.SerializeObject(RandomString.PseudoRandom.Generate(12)));
    }
  }
}
