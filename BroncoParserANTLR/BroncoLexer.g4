lexer grammar BroncoLexer;

TITLE_NAME: ID;
TITLE_CLOSE: WS* '=' WS* '\n' -> mode(WS_BAG_MODE);
TITLE_OPEN: WS* '=' WS*;
WHITESPACE: [ \t\n] -> skip;

mode WS_BAG_MODE;
LINE_CLOSE: '\n';
BAG_CLOSE: '\n' WS* '\n' -> mode(DEFAULT_MODE);
NON_TERMINAL: '<' ID '>';
TERMINAL: TERMINAL_CHAR+;

fragment ID: [A-Za-z][A-Za-z0-9_]*;
fragment WS: [ \t];
fragment TERMINAL_CHAR: ~[\n<#%];