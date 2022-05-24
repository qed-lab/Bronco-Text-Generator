using BroncoLibrary;
using BroncoTest;

VariableSetter setter = new VariableSetter();
SymbolVariable pickedAnimal = new SymbolVariable();

Bag animalBag = (Bag) new P()
{
    "dog", 
    "cat", 
    "rabbit", 
    "dragon",
    "mouse",
    "parrot",
    "lizard",
    "spider",
    "snake"
};

animalBag.Add(new MetaData<ISymbol>((SymbolList)new P() { animalBag, "-", animalBag }, 1.5));

Bag adjectiveBag = (Bag) new P()
{
    "small",
    "large",
    "cute",
    "scary",
    (SymbolList) new P() {"much like a ", animalBag},
};
adjectiveBag.Add(new MetaData<ISymbol>((SymbolList) new P() { "very ", adjectiveBag }, 1.5));

ISymbol root = (SymbolList)new P()
{
    "This is a story about a ", setter.Argue(new ISymbol[]{pickedAnimal, animalBag}), 
    ". This particular ", pickedAnimal, " looked ", adjectiveBag
};

Console.WriteLine(root.Flatten().Value);