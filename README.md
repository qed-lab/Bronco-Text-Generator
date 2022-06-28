# Syntax
Bronco is a programming language for building powerful text generators easily. Inspired by Tracery, it is a grammar-based text-substitution system at its heart, but is capable of much more.

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
**Sample output**`second item`

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
**Sample output**`I randomly choose the color dark red`

### Arguments
When making a reference, you can include arguments which will effect the output. These are denoted with a colon after the reference, followed by a number of comma-separated arguments `<reference: argument1, arg2, otherArg>`. Similarly, it is possible add arguments to a bag, and to use that argument in the output. Arguments can either be, literals, references to other symbols, or nested arguments denoted with parentheses instead of triangle braces `<bag: (otherBag: `argument`)>`.

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
**Sample output**`Tell me about using arguments and blue!`
