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

@complement: col
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

@colors: values
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
greater than 5| (gt: input, 5) |
less than 5| (lt: input, 5) |
```
**Sample output** `This number is greater than 5!`

## Advanced: Battle

```
@start
<enemyIntro> <heroIntro> <battle>

/*intro*/

@heroIntro
<silent: (set:heroHP, 50)><silent: (set:heroWeapon, weaponTypes)>You <heroWeaponIntro: heroWeapon>, and prepare to fight.

@heroWeaponIntro: weapon
heft your <weapon>#axe #club
draw your <weapon>#sword
string your <weapon>#bow

@enemyIntro
A <enemyTypes> appears from the darkness.

@enemyTypes
<set: enemy, beastEnemies>
<set: enemy, huminoidEnemies> with a <enemyWeapon>

@huminoidEnemies
goblin<silent: (set:enemyWeapon, weaponTypes)><silent: (set:enemyHP, 30)>
orc<silent: (set:enemyWeapon, weaponTypes)><silent: (set:enemyHP, 50)>
skeleton<silent: (set:enemyWeapon, weaponTypes)><silent: (set:enemyHP, 25)>
troll<silent: (set:enemyWeapon, weaponTypes)><silent: (set:enemyHP, 80)>

@beastEnemies
viper<silent: (set:enemyWeapon, ` #bite`)><silent: (set:enemyHP, 20)>
emu<silent: (set:enemyWeapon, ` #bite #claw`)><silent: (set:enemyHP, 40)>
wolf<silent: (set:enemyWeapon, ` #bite #claw`)><silent: (set:enemyHP, 60)>
wraith<silent: (set:enemyWeapon, ` #claw`)><silent: (set:enemyHP, 70)>

@weaponTypes
axe#axe
sword#sword
bow#bow
club#club

/*battle*/
@battle
You take the first move! <heroTurn>
The <enemy> takes the first move! <enemyTurn>

@heroTurn
<heroMove: heroWeapon> <enemyReaction>     <if: (gt: enemyHP, 0), enemyTurn, heroVictory>

@enemyTurn
<enemyMove: enemyWeapon> <heroReaction>     <if: (gt: heroHP, 0), heroTurn, enemyVictory>

@heroMove: weapon
You take a slash with your <weapon>.#sword #axe
You make a lunge with your <weapon>.#sword
You bring your <weapon> down with a smash.#axe #club
You swing your <weapon>.#sword #axe #club
You fire an arrow from your <weapon>.#bow
You let loose a volley of arrow from your <weapon>.#bow

@heroReaction
The attack lands hard! You take <set: dmg, (randomI:30, 60)> damage, leaving you <silent: (set: heroHP, (sub: heroHP, dmg))><damage: heroHP>
The attack only grazes you. You take <set: dmg, (randomI:10, 20)> damage, leaving you <silent: (set: heroHP, (sub: heroHP, dmg))><damage: heroHP>
The attack misses leaving you unscathed. 

@heroVictory
Gravely wounded, you stagger over the corpse of the <enemy>, away from the hard won fight...|(lt: heroHP, 15)|
With a few new scars, you walk over the corpse of the <enemy>, away from the battle.|(and: (gt: heroHP, 15), (lt: heroHP, 41)) |
With a smile, you walk confidently over the corpse of the <enemy>, away from an easy victory.|(gt: heroHP, 40)|

@enemyMove: weapon
The <enemy> slashes at you with its claws.#claw
The <enemy> claws at you.#claw
The <enemy> leaps to take a bite.#bite 
The <enemy> bites at you.#bite 
The <enemy> swings at you with its <weapon>.#axe #club #sword
The <enemy> brings its <weapon> down at your head.#axe #club #sword
The <enemy> fires an arrow from its <weapon>.#bow
The <enemy> looses a quick vollow of arrows from its <weapon>.#bow

@enemyReaction
The attack hits the <enemy> hard! It takes <set: dmg, (randomI:30, 60)> damage, leaving it <silent: (set: enemyHP, (sub: enemyHP, dmg))><damage: enemyHP>
The attack only grazes the <enemy>. It takes <set: dmg, (randomI:10, 20)> damage, leaving it <silent: (set: enemyHP, (sub: enemyHP, dmg))><damage: enemyHP>
The attack misses, leaving the <enemy> unscathed.

@enemyVictory
Gravely wounded, the <enemy> staggers away from the bitter fight...|(lt: enemyHP, 15)|
The <enemy> stoops over to devour your corpse! |(matchTag: ` #bite`, enemyWeapon)|
The <enemy> seems pleased by the victory, takes your <heroWeapon> for its own! |(matchTag: ` #axe #club #sword #bow`, enemyWeapon)|

@damage: hp
with <hp> HP.|(gt: hp, 0)|
dead!|(lt: hp, 1)|
```
**Sample output** `A goblin with a sword appears from the darkness. You draw your sword, and prepare to fight. The goblin takes the first move! The goblin swings at you with its sword. The attack lands hard! You take 31 damage, leaving you with 19 HP.     You take a slash with your sword. The attack hits the goblin hard! It takes 51 damage, leaving it dead!     With a few new scars, you walk over the corpse of the goblin, away from the battle.`
