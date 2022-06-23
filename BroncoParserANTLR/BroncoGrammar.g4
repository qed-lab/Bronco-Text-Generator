grammar BroncoParser;

/*
PARSER
*/

start: ID*;

/*
LEXER
*/

ID: [A-Za-z][A-Za-z0-9_]*;
TERMINAL: '"'~["]+'"';
NL: '\n';
WS: [ \t]+ -> skip;

AT: '@';
HASH: '#';
PERCENT: '%';
COLON: ':';
COMMA: ',';
LT: '<';
GT: '>';
