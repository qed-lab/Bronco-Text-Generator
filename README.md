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

### Inserts
Triangle braces can be used to insert dynamic elements into text `<reference>`. Most commonly, these inserts are a reference to another bag. These references can also be recursive.

```
@start
I randomly choose the color <color>

@color
red
green
blue
dark <color>
```
