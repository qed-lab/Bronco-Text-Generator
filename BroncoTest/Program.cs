using BroncoLibrary;

Bag animalBag = new Bag()
{
    new MetaTerminal("dog"),
    new MetaTerminal("cat"),
    new MetaTerminal("rabbit"),
    new MetaTerminal("dragon"),
};

Bag adjectiveBag = new Bag()
{
    new MetaTerminal("large"),
    new MetaTerminal("small"),
    new MetaTerminal("cute"),
    new MetaTerminal("scary"),
    new SymbolList(){new MetaTerminal("much like a "), animalBag},
};
adjectiveBag.Add(new SymbolList() { new MetaTerminal("very "), adjectiveBag });

SymbolList root= new SymbolList()
{
    new MetaTerminal("The "), animalBag, new MetaTerminal(" looked "), adjectiveBag
};

Console.WriteLine(((ISymbol)root).GetString());
Console.WriteLine(((ISymbol)root).GetString());
Console.WriteLine(((ISymbol)root).GetString());
Console.WriteLine(((ISymbol)root).GetString());
