lexer grammar broncoLexer;

fragment LOWER: [a-z];
fragment UPPER: [A-Z];
fragment DIGIT: [0-9];
fragment ID: (LOWER | UPPER)(LOWER | UPPER | DIGIT | '_')*;
fragment WHITESPACE: (' '|'\t')+;
fragment NEWLINE: ('\r\n'|'\n'|'\r');

TITLE_OPEN: WHITESPACE*'='WHITESPACE*;
TITLE_TEXT: ID;
TITLE_CLOSE: WHITESPACE*'='WHITESPACE* NEWLINE -> mode(ITEM);
IGNORE: (NEWLINE | WHITESPACE) -> skip;

mode ITEM;
CALL_OPEN: '<' -> mode(CALL);
TERMINAL: ~[\r\n<]+;

mode CALL;
CALL_INNER: ~[>]+;
CALL_CLOSE: '>' -> mode(ITEM);