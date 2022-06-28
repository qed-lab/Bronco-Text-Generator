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
chartreuse#red:2.23#green:2.55#blue:0
seafoam green#red:1.59#green:2.26#blue:1.91
mahogany#red:1.92#green:0.64#blue:0.0
orchid#red:2.18#green:1.12#blue:2.14
violet#red:1.27#green:0#blue:2.55
salmon#red:2.5#green:1.18#blue:1.14

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
