# Examples

Here are a bunch of examples of different concepts in Bronco being put to use. They are roughly in increasing order of complexity. This is a good place to start if you want to learn how to use the language.

## Basic Bags

```
@start
I randomly choose the color <color>

@color
red
green
blue
dark <color>
```
**Sample output** `I randomly choose the color dark red`

## Basic Arguments

```
@start
<tell: `using arguments`,color>

@tell: arg1, arg2
Tell me about <arg1> and <arg2>!

@color
red
green
blue
```
**Sample output** `Tell me about using arguments and blue!`

## Pre-Built Symbols

```
@start
I’ll remember the color <set: var, color>, later. Two plus two is: <add: 2, 2>. Four times two is <mult: 4, 2>. A random integer between 1 and 10: <randomI: 1, 10>. The color I picked earlier was <var>.

@color
red
green
blue
```
**Sample output** `I’ll remember the color blue, later. Two plus two is: 4. Four times two is 8. A random integer between 1 and 10: 8. The color I picked earlier was blue.`

## Weights

```
@start
Not very likely%0.25
Quite likely%1.72
```
**Sample output** `Quite likely`

## Simple Tags

```
@start
The compliment of <set: picked, color> is <complement: picked>

@color
red#red
yellow#yellow
blue#blue

@complement: col [tagOverlap: col, item]
green#red
purple#yellow
orange#blue
```
**Sample output** `The compliment of blue is orange`

## Advanced Tags
```
@start
I'm looking for something <pickPreference>, like <colors: preference>

@pickPreference
<set: preference, colorValues>
<set: preference, colorValues> and <addTag: preference, colorValues>

@colorValues
red#red
green#green
blue#blue

@colors: values [tagMult: col, item]
chartreuse#red:2.23 #green:2.55 #blue:0
seafoam green#red:1.59 #green:2.26 #blue:1.91
mahogany#red:1.92 #green:0.64 #blue:0.0
orchid#red:2.18 #green:1.12 #blue:2.14
violet#red:1.27 #green:0 #blue:2.55
salmon#red:2.5 #green:1.18 #blue:1.14
```
**Sample output** `I'm looking for something green and blue, like orchid`

## Basic Conditions

```
@start
This number is <num: 7>!

@num: input
greater than 5[ gt: input, 5 ]
less than 5[ lt: input, 5 ]
```
**Sample output** `This number is greater than 5!`

## Advanced: Battle

```
@start
<enemyIntro> <heroIntro> <battle>

/*intro*/

@heroIntro
<$ heroHP = 50><$ heroWeapon = weaponTypes>You <heroWeaponIntro: heroWeapon>, and prepare to fight.

@heroWeaponIntro: weapon[tagOverlap: weapon, item]
heft your <weapon>#axe #club
draw your <weapon>#sword #gun
string your <weapon>#bow

@enemyIntro
<$ enemyHP = 50><enemy = enemyTools> appears from the darkness.

@enemyTools
Tracery<$ enemyWeapon = ` #easy #variables #javascript #contextfreegrammar`>%1.25
Expressionist<$ enemyWeapon = ` #tags #customizability #attributegrammar`>%1.25
Bronco<$ enemyWeapon = ` #tags #variables #functions #customizability #adaptivegrammar`><$ enemyHP = 100>%1.25
Step<$ enemyWeapon = ` #tags #variables #functions #taskPlanning #logic`>%1.25
Blabbeur<$ enemyWeapon = ` #variables #unity #functions #contextfreegrammar`>
MKULTRA<$ enemyWeapon = ` #logic #definiteclausegrammar`>
Improv<$ enemyWeapon = ` #javascript #filtering #contextfreegrammar`>
SimpleNLG<$ enemyWeapon = ` #nlg #easy`>
Dunyazad<$ enemyWeapon = ` #definiteclausegrammar`>

@weaponTypes
axe of <idn>#axe
sword of <idn>#sword
bow of <idn>#bow
club of <idn>#club
Chekhov's gun#gun

@idn
interactive narrative
procedural storytelling
social simulation
education through IDN
ludonarrative dissonance
planning

/*battle*/
@battle
You take the first move! <heroTurn>
The <enemy> takes the first move! <enemyTurn>

@heroTurn
<heroMove: heroWeapon> <enemyReaction>     <(gt: enemyHP, 0)? enemyTurn; heroVictory>

@enemyTurn
<enemyMove: enemyWeapon> <heroReaction>     <(gt: heroHP, 0)? heroTurn; enemyVictory>

@heroMove: weapon[tagOverlap: weapon, item]
You take a slash with your <weapon>.#sword #axe
You make a lunge with your <weapon>.#sword
You bring your <weapon> down with a smash.#axe #club
You swing your <weapon>.#sword #axe #club
You fire an arrow from your <weapon>.#bow
You let loose a volley of arrow from your <weapon>.#bow
You fire <weapon>.#gun


@heroReaction
The attack lands hard! You take <dmg = randomI:30, 60> damage, leaving you <$ heroHP = sub: heroHP, dmg><damage: heroHP>
The attack only grazes you. You take <dmg = randomI:10, 20> damage, leaving you <$ heroHP = sub: heroHP, dmg><damage: heroHP>
The attack misses leaving you unscathed. 

@heroVictory
Gravely wounded, you stagger over the corpse of <enemy>, away from the hard won fight...[(lt: heroHP, 15)]
With a few new scars, you walk over the corpse of <enemy>, away from the battle.[and: (gt: heroHP, 15), (lt: heroHP, 41)]
With a smile, you walk confidently over the corpse of <enemy>, away from an easy victory.[gt: heroHP, 40]

@enemyMove: weapon[tagOverlap: weapon, item]
<enemy> unleashes a volley of tags.#tags
<enemy> tags you.#tags
<enemy> assigns variables at you.#variables
<enemy> attacks with variables.#variables
<enemy> attacks you using JavaScript.#javascript 
<enemy> extends itself with custom functionality.#customizability 
<enemy> attacks you with its imperative logic.#logic
<enemy> uses its planning system for a highly strategic attack.#taskPlanning
<enemy> attacks you with its context-free-grammar.#contextfreegrammar
<enemy> attacks you with its imperative-adaptive-grammar.#adaptivegrammar
<enemy> attacks you with its attribute-grammar.#attributegrammar
<enemy> functionally attacks you.#functions
<enemy> attacks you with its definite-clause-grammar.#definiteclausegrammar
<enemy> sways you with its natural language.#nlg
<enemy> sneakily attacks you with its ease of use.#easy


@enemyReaction
The attack hits <enemy> hard! It takes <dmg = randomI:30, 60> damage, leaving it <$enemyHP = sub: enemyHP, dmg><damage: enemyHP>
The attack only grazes <enemy>. It takes <dmg = randomI:10, 20> damage, leaving it <$ enemyHP = sub: enemyHP, dmg><damage: enemyHP>
The attack misses, leaving <enemy> unscathed.

@enemyVictory
Gravely wounded, <enemy> staggers away from a hard won fight...[lt: enemyHP, 15]
With a few new scars, <enemy> walks over your corpse, away from the battle.[and: (gt: enemyHP, 15), (lt: enemyHP, 41)]
With a smile, <enemy> walks confidently over your corpse, away from an easy victory.[gt: enemyHP, 40]

@damage: hp
with <hp> HP.[gt: hp, 0]
dead![lt: hp, 1]
```
**Sample output** `Tracery appears from the darkness. You heft your axe of planning, and prepare to fight. You take the first move! You bring your axe of planning down with a smash. The attack misses, leaving Tracery unscathed.     Tracery assigns variables at you. The attack misses leaving you unscathed.      You bring your axe of planning down with a smash. The attack misses, leaving Tracery unscathed.     Tracery attacks you with its context-free-grammar. The attack lands hard! You take 42 damage, leaving you with 8 HP.     You bring your axe of planning down with a smash. The attack misses, leaving Tracery unscathed.     Tracery assigns variables at you. The attack only grazes you. You take 15 damage, leaving you dead!     With a smile, Tracery walks confidently over your corpse, away from an easy victory.`
