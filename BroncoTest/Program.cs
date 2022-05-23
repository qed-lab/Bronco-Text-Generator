using BroncoLibrary;

Bag animalBag = new Bag()
{
    new MetaData<Symbol>(new Terminal("dog")),
    new MetaData<Symbol>(new Terminal("cat")),
    new MetaData<Symbol>(new Terminal("rabbit")),
    new MetaData<Symbol>(new Terminal("dragon")),
};

Bag adjectiveBag = new Bag()
{
    new MetaData<Symbol>(new Terminal("small")),
    new MetaData<Symbol>(new Terminal("large")),
    new MetaData<Symbol>(new Terminal("cute")),
    new MetaData<Symbol>(new Terminal("scary")),
    new MetaData<Symbol>(new SymbolList(){new Terminal("much like a "), animalBag}),
};
adjectiveBag.Add(new MetaData<Symbol>(new SymbolList() { new Terminal("very "), adjectiveBag }, 2));

SymbolList root = new SymbolList()
{
    new Terminal("The "), animalBag, new Terminal(" looked "), adjectiveBag
};

Console.WriteLine(root.EvaluateString(Symbol.EmptyArgs));
Console.WriteLine(root.EvaluateString(Symbol.EmptyArgs));
Console.WriteLine(root.EvaluateString(Symbol.EmptyArgs));
