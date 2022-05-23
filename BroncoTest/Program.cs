using BroncoLibrary;
using BroncoTest;

Bag animalBag = (Bag) new P()
{
    "dog", 
    "cat", 
    "rabbit", 
    "dragon"
};

Bag adjectiveBag = (Bag) new P()
{
    "small",
    "large",
    "cute",
    "scary",
    (SymbolList) new P() {"much like a ", animalBag},
};
adjectiveBag.Add(new MetaData<Symbol>((SymbolList) new P() { "very ", adjectiveBag }, 1.5));

SymbolList root = (SymbolList)new P()
{
    "The", animalBag, " looked ", adjectiveBag
};

Console.WriteLine(root.EvaluateString(Symbol.EmptyArgs));
Console.WriteLine(root.EvaluateString(Symbol.EmptyArgs));
Console.WriteLine(root.EvaluateString(Symbol.EmptyArgs));
