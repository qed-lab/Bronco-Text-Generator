# Bronco Language Reference

What follows is a quick and dirty handbook for anybody wanting to learn how to write generators in Bronco. If you’ve never used it before, I’d encourage you to start by looking at the [examples](EXAMPLES.md), and coming back here as needed. 

## Syntax

### Bags
Each Bronco file is made up of a number of *bags*. Every bag is denoted with an at symbol `@bagTitle`, followed by a number of newline separated items, which is mostly normal text. 

When a bag is evaluated, an item will be picked from it at random. All Bronco programs start from the `@start` bag.

```
@start
first item
second item
third item

@anotherBag
more items
one last one
```
**Sample output** `second item`

### Inserts
Triangle braces can be used to insert dynamic elements into text `<insert>`. Most commonly, these inserts are a reference to another bag, but can reference other symbols as well. These references can also be recursive.

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

### Arguments
When making a reference, you can include arguments which will effect the output. These are denoted with a colon after the reference, followed by a number of comma-separated arguments `<reference: argument1, arg2, otherArg>`. Similarly, it is possible add arguments to a bag, and to use that argument in the output. Arguments can either be, literals, references to other symbols, or nested arguments ``<bag: otherBag: `argument`>``. Parenthesis can be added to symbol-calls to avoid ambiguity ``<bag: (otherBag: `argument`), `second argument for bag`>``
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

### Built-In Symbols
In addition to the bags you define, there are many pre-built symbols you can access. These typically perform operations or other concise functionality. Although Bronco is made entirely of symbols under the hood, some of the pre-built symbols can be used with special syntax for ease of use.

Of particular note is the *set* symbol (which can be written ``this = that``), which let’s you define and change the value of variables. A full list of pre-built symbols is found at the end of the document.

```
@start
I’ll remember the color <var = color>, later. Two plus two is: <add: 2, 2>. Four times two is <mult: 4, 2>. A random integer between 1 and 10: <randomI: 1, 10>. The color I picked earlier was <var>.

@color
red
green
blue
```
**Sample output** `I’ll remember the color blue, later. Two plus two is: 4. Four times two is 8. A random integer between 1 and 10: 8. The color I picked earlier was blue.`

### Conditions
For explicit, dynamic control over the probability of an item being picked, you may specify a condition. This is denoted with some symbol inside of square brackets after an item `item [ conditionSymbol ]`. Conditions must resolve to either a float, which is then multiplied with the base weight, or a a boolean, which is equivalent to either 1.0 or 0.0. 

```
@start
This number is <num: 7>!

@num: input
greater than 5[ gt: input, 5 ]
less than 5[ lt: input, 5 ]
```
**Sample output** `This number is greater than 5!`

### Weights
You can use weights to change the chances of an item being picked from a bag. This is denoted by a percent sign followed by a floating point number `%1.5`. The last example would make an item 50 percent more likely to be picked, where `%0.5` would half its chances.

```
@start
Not very likely%0.25
Quite likely%1.72
```
**Sample output** `Quite likely`

### Tags
For more complex control over probability, you can attach tags to items in a list. This is denoted with a pound sign followed by the name of the tag `#tag`. Tags themselves can also have weights `#tag:1.5` (not specifying a weight counts as a weight of 1.0)

Tags have no inherent functionality whatsoever, which means their function needs to be manually specified. This is done through bag-wide-conditions, denoted with square brackets after the title and arguments of a bag `@bag: arg [tagOverlap: arg, item]`. These bag-wide-conditions can access arguments, and use the keyword `item` to reference the item in the bag being considered. The conditions can also be used to execute arbitrary functionality for every item, and don’t actually need to reference tags at all. 

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

## Pre-built symbols
What follows is a list of the symbols and their functions (and shortcuts if they exist), that come pre-defined in Bronco.

`<set: variable, value>`: Sets symbol held in `var` to the flatted `value`. Evaluates to the new value. Equivalent to `<variable = value>`.

`<do: symbol>`: Evaluates the given symbol without outputting anything. Evaluates to an empty symbol. Equivalent to `<$ symbol>`.

`<addTag: to, from>`: Adds the tags from `from` to the tags in `to`. Evaluates to `from`.

`<removeTag: remove, removeFrom>`: Removes the tags from `remove` from the tags in `removeFrom`. Evaluates to `removeFrom`.

`<choose: item1, item2, item3>`: Taking any number of arguments, evaluates to one item picked at random.

`<setPointer: variable, value>`: Like `set`, except that it points `variable` to `value` without flattening it first. (i.e. if you point `variable` to a bag, it will continue evaluating a random item every time `variable` is references)

`<if: condition, onTrue, onFalse>`: Taking a boolean input, evaluates to `onTrue` if `condition` is true, and `onFalse` otherwise. Equivalent to `<condition? onTrue; onFalse>`.

`<gt: num1, num2>`: Evaluates true if float `num1` is greater than float `num2`, false otherwise.

`<lt: num1, num2>`: Evaluates true if float `num1` is less than float `num2`, false otherwise.

`<equal: num1, num2>`: Evaluates true if float `num1` is equal to float `num2`, false otherwise.

`<add: num1, num2>`: Evaluates ro the result of float `num1` plus to float `num2`.

`<sub: num1, num2>`: Evaluates ro the result of float `num1` minus float `num2`.

`<mult: num1, num2>`: Evaluates ro the result of float `num1` times float `num2`

`<random: upper>` `<random: lower, upper>`: Evaluates a random float between 0 and `upper` or between `lower` and `upper`.

`<randomI: upper>` `<randomI: lower, upper>`: Evaluates a random integer between 0 and `upper` or between `lower` and `upper`.

`<round: num>`: Evaluates to float `num` rounded to the nearest integer.

`<and: bool1, bool1>`: Evaluates true if both `bool1` and `bool2` are true, false otherwise.

`<or: bool1, bool1>`: Evaluates true if either `bool1` or `bool2` are true, false otherwise.

`<not: bool>`: Evaluates true if `bool` is false, false otherwise.

`<doYield: symbol>`: Like `do`, except that it evaluates to the final argument given instead of the empty symbol.

`<addToBag: addTo, item, condition>`: Adds `item` and the optional `condition` parameter, and adds them to the `addTo` bag. 

`<tagMult: symbol1, symbol2>`: Tag filter. Evaluates to all the weights of the tags `symbol1` and `symbol2` have in common multiplied together.

`<tagContains: symbol1, symbol2>`: Tag filter. Evaluates like `tagMult` if `symbol1` contains the entirety of `symbol2`, 0 otherwise.

`<tagNoOverlap: symbol1, symbol2>`: Tag filter. Evaluates to 1 if `symbol1` and `symbol2` have no tags in common, 0 otherwise.

`<tagOverlap: symbol1, symbol2>`: Tag filter. Evaluates like `tagMult` if `symbol1` and `symbol2` have at least 1 tag in common, 0 otherwise.

`<cap: symbol>`: Evaluates to `symbol`, flattened and with its first letter capitalized.

`<a: symbol>`: Evaluates to `symbol`, flattened and prepended with either `a` or `an` according to standard English rules. 

`<s: symbol>`: Evaluates to `symbol`, flattened and pluralized according to standard English pluralization rules.

`<ed: symbol>`: Evaluates to `symbol`, flattened and made passed-tense according to standard English rules for verbs.










