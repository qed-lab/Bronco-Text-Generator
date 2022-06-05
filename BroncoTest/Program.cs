using BroncoLibrary;
using BroncoParser;
using BroncoTest;

VariableSetter setter = new VariableSetter();
SymbolVariable pickedAnimal = new SymbolVariable();

/*
Bag adjectives = new Bag();
adjectives.Add(new MetaData<ISymbol>(new Terminal("Theoretical"), new string[] { "theory" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Applied"), new string[] { "applied" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Procedural"), new string[] { "procGen" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Abstract"), new string[] { "theory", "math" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Ethical"), new string[] { "ethics" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Moral"), new string[] { "ethics" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Modeled"), new string[] { "applied", "theory" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Heuristic"), new string[] { "algs" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Algorithmic"), new string[] { "algs" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Algebraic"), new string[] { "math" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Combinitoric"), new string[] { "math" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Secure"), new string[] { "security" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Encrypted"), new string[] { "security" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Intellegent"), new string[] { "ai" }));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Theoretical"), 0.4));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Applied"), 0.4));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Modeled"), 0.4));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Intellegent"), 0.4));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Abstract"), 0.4));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Intentional")));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Serious"), 0.75));
adjectives.Add(new MetaData<ISymbol>(new Terminal("Modern")));
adjectives.Add(new MetaData<ISymbol>(new SymbolList() { new Terminal("Highly "), adjectives }, 0.75));

Bag researchNouns = new Bag();
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Artificial Intelligence"), new string[] { "ai" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Neural Networks"), new string[] { "ai", "algs" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Machine Learning"), new string[] { "ai" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Algorithms"), new string[] { "algs" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Binary Trees"), new string[] { "algs" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Graphics"), new string[] { "graphics" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Data Structures"), new string[] { "algs" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Lighting Simulations"), new string[] { "graphics" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Raytracing"), new string[] { "graphics" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Procedural Textures"), new string[] { "graphics", "procGen" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Ethics"), new string[] { "ethics" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Morality"), new string[] { "ethics" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Programming Languages")));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Software")));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Procedural Generation"), new string[] { "procGen" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Proceduralism"), new string[] { "procGen" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Perlin Noise"), new string[] { "procGen" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Grammars"), new string[] { "procGen", "math", "algs" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Hardware"), new string[] { "hardware" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Processors"), new string[] { "hardware" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Graphics Cards"), new string[] { "hardware", "graphics" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Mathimatics"), new string[] { "math", "theory" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Calculus"), new string[] { "math" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Matricies"), new string[] { "math" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Cyper Security"), new string[] { "security" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Encryption"), new string[] { "security", "algs", "math" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Cryptography"), new string[] { "security" }));
researchNouns.Add(new MetaData<ISymbol>(new Terminal("Blockchain"), new string[] { "security" }));

/*
 * ai
 * procGen
 * algs
 * graphics
 * hardware
 * ethics
 * theory
 * applied
 * security
 * math
 */
/*
MetaData<ISymbol>[] tags = new MetaData<ISymbol>[] { 
    new MetaData<ISymbol>(new Terminal(""), new string[] {"ai", "algs", "ethics", "procGen", "hardware", "theory", "applied", "security", "math"}) };

Bag adjNoun = new Bag();
Bag paper = new Bag();

adjNoun.Add(new MetaData<ISymbol>(new SymbolList() { adjectives.Argue(tags), new Terminal(" "), researchNouns.Argue(tags) }));
adjNoun.Add(new MetaData<ISymbol>(researchNouns.Argue(tags), 0.5));
adjNoun.Add(new MetaData<ISymbol>(paper, 0.25));

paper.Add(new MetaData<ISymbol>(new SymbolList() { adjNoun, new Terminal(" for "), adjNoun }));
paper.Add(new MetaData<ISymbol>(new SymbolList() { adjNoun, new Terminal(" in "), adjNoun }));
paper.Add(new MetaData<ISymbol>(new SymbolList() { adjNoun, new Terminal(" and "), adjNoun }));
paper.Add(new MetaData<ISymbol>(new SymbolList() { new Terminal("The Relation Between "), adjNoun, new Terminal(" and "), adjNoun }));

Console.WriteLine(((ISymbol)paper).Flatten().Value);
Console.WriteLine(((ISymbol)paper).Flatten().Value);
Console.WriteLine(((ISymbol)paper).Flatten().Value);
Console.WriteLine(((ISymbol)paper).Flatten().Value);
*/
/*
ISymbol root = GeneratorParser.ParseString(
@"=start=
test
");
*/

ISymbol root = GeneratorParser.ParseString(
@"=start=
The <animal> looked <adjective>

=animal=
dog
cat
bird
rat
<animal>-<animal>

=adjective=
big
small
scary
cute
very <adjective>
<adjective> and <adjective>
much like a <animal>");


Console.WriteLine(root.Flatten().Value);
Console.WriteLine(root.Flatten().Value);
Console.WriteLine(root.Flatten().Value);
Console.WriteLine(root.Flatten().Value);
Console.WriteLine(root.Flatten().Value);


//GeneratorParser.Test();

