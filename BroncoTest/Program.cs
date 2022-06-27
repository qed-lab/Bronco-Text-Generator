using BroncoLibrary;
using BroncoParserANTLR;
using Antlr4.Runtime;


ISymbol output = BroncoParser.Parse(File.OpenRead("Test.bronco"));

Console.WriteLine(output.Flatten().Value);
Console.WriteLine();
Console.WriteLine();
