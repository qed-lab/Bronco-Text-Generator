using BroncoLibrary;
using BroncoTest;

VariableSetter setter = new VariableSetter();
SymbolVariable pickedAnimal = new SymbolVariable();

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
    "This is a story about a ", new ArgumentHolder(setter, new Symbol[]{pickedAnimal, animalBag}), 
    ". This particular ", pickedAnimal, " looked ", adjectiveBag
};

Console.WriteLine(root.Flatten(Symbol.EmptyArgs));
Console.WriteLine(root.Flatten(Symbol.EmptyArgs));
Console.WriteLine(root.Flatten(Symbol.EmptyArgs));
