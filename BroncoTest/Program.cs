using BroncoLibrary;
using BroncoParserANTLR;
using Antlr4.Runtime;
using BroncoTextParser;


ISymbol output = TextParser.Parse(File.OpenRead("Test.bronco"));

Console.WriteLine(output.Flatten().Value);
Console.WriteLine();
Console.WriteLine();
