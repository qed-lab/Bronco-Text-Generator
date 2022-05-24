using BroncoLibrary;
using BroncoTest;

VariableSetter setter = new VariableSetter();
SymbolVariable pickedAnimal = new SymbolVariable();

Bag animalBag = (Bag) new P()
{
    "dragon",
    "parrot",
    "lizard",
    "spider",
    "snake"
};
animalBag.Add(new MetaData<ISymbol>((SymbolList)new P() { animalBag, "-", animalBag }, 1.5));
animalBag.Add(new MetaData<ISymbol>(new Terminal("Rabbit"), new string[] { "furry" }));
animalBag.Add(new MetaData<ISymbol>(new Terminal("Dog"), new string[] { "furry" }));
animalBag.Add(new MetaData<ISymbol>(new Terminal("Cat"), new string[] { "furry" }));
animalBag.Add(new MetaData<ISymbol>(new Terminal("Mouse"), new string[] { "furry" }));

Bag adjectiveBag = (Bag) new P()
{
    "small",
    "large",
    "cute",
    "scary",
    (SymbolList) new P() {"much like a ", animalBag},
};
adjectiveBag.Add(new MetaData<ISymbol>((SymbolList) new P() { "very ", adjectiveBag }, 1.5));

ISymbol[] tag = new ISymbol[]
{
        new MetaData<ISymbol>(new Terminal("Does not matter"), new string[] { "furry" })
};


ISymbol root = (SymbolList)new P()
{
    "This is a story about a ", setter.Argue(new ISymbol[]{pickedAnimal, animalBag.Argue(tag) }), 
    ". This particular ", pickedAnimal, " looked ", adjectiveBag
};

Console.WriteLine(root.Flatten().Value);