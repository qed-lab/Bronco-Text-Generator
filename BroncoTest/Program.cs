using BroncoLibrary;

Bag animalBag = new Bag();
animalBag.Add(new MetaTerminal("cat"));
animalBag.Add(new MetaTerminal("dog"));
animalBag.Add(new MetaTerminal("rabbit"));
animalBag.Add(new MetaTerminal("dragon"));

Bag adjectiveBag = new Bag();
adjectiveBag.Add(new MetaTerminal("small"));
adjectiveBag.Add(new MetaTerminal("large"));
adjectiveBag.Add(new MetaTerminal("scary"));
adjectiveBag.Add(new MetaTerminal("cute"));

SymbolList root= new SymbolList();
root.Add(new MetaTerminal("The "));
root.Add(animalBag);
root.Add(new MetaTerminal(" looked "));
root.Add(adjectiveBag);

Console.WriteLine(((ISymbol) root).GetString());
