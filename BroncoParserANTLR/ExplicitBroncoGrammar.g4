grammar ExplicitBroncoGrammar;

/*
PARSER
*/

start: file;

file: bag+ EOF;

bag: bag_title bag_item+;
bag_title: AT ID bag_args?;
bag_args: COLON ID (COMMA ID)*;
bag_item: TILDA symbol (PIPE symbol_ref)?;
symbol: (TERMINAL | symbol_ref)+ meta_data*;

symbol_call: LT symbol_ref symbol_call_args? GT;
symbol_call_args: COLON symbol_ref (COMMA symbol_ref)*;
symbol_ref: ID | symbol_call;

meta_data: meta_tag | meta_weight;
meta_tag: HASH ID (COLON FLOAT)?;
meta_weight: PERCENT FLOAT;

/*
LEXER
*/

ID: [A-Za-z][A-Za-z0-9_]*;
TERMINAL: '"'~["]+'"';
FLOAT: INT ('.' INT)?;
INT: [0-9]+;

WS: [ \t\n]+ -> skip;
AT: '@';
HASH: '#';
PERCENT: '%';
COLON: ':';
COMMA: ',';
LT: '<';
GT: '>';
TILDA: '~';
PIPE: '|';
