lexer grammar BroncoLexer;

TITLE_NAME: ID;
TITLE_CLOSE: WS* '=' WS* '\n' -> mode(WS_BAG_MODE);
TITLE_OPEN: WS* '=' WS*;
WHITESPACE: [ \t\n] -> skip;

mode WS_BAG_MODE;
LINE_CLOSE: '\n';
BAG_CLOSE: '\n' WS* '\n' -> mode(DEFAULT_MODE);
NON_TERMINAL_OPEN: '<' -> mode(NON_TERMINAL_MODE);
TERMINAL: TERMINAL_CHAR+;

mode NON_TERMINAL_MODE;
TARGET: ID;
ARG_START: WS* ':' WS*;
ARG_SEPERATE: WS* ',' *WS;
NON_TERMINAL_CLOSE: '>' -> mode(WS_BAG_MODE);

fragment ID: [A-Za-z][A-Za-z0-9_]*;
fragment DIGIT: [0-9];
fragment FLOAT: DIGIT+('.'DIGIT+)?;
fragment WS: [ \t];
fragment TERMINAL_CHAR: ~[\n<#%];