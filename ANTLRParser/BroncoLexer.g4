lexer grammar BroncoLexer;

/*
LEXER
*/

fragment ID: [A-Za-z][A-Za-z0-9_]*;
fragment NUM: '-'?[0-9]+ ('.' [0-9]+)?;
fragment NL: '\r'?'\n';

TITLE: '@'ID -> mode(TITLE_MODE);
BOOL_LITERAL: 'true' | 'false';
IDENTIFIER: ID;
NUMBER: NUM;

SKIP_COMMENT: '/*'~[*]+'*/' -> skip;
SKIP_WS: [ \t\n\r]+ -> skip;

COLON: ':';
COMMA: ',';
OP: '(';
CP: ')';
GT: '>' -> mode(TERMINAL_MODE);
START_QUOTE: '`' -> mode(TERMINAL_MODE);
CLOSE_PIPE: '|' -> mode(TERMINAL_MODE);

mode TITLE_MODE;
TITLE_COLON: COLON;
TITLE_COMMA: COMMA;
TITLE_ID: ID;
TITLE_SKIP_WS: [ \t]+ -> skip;
TITLE_NEWLINE: NL -> mode(TERMINAL_MODE);

mode TERMINAL_MODE;
LT: '<' -> mode(DEFAULT_MODE);
META_TAG: '#' ID (COLON NUM)?;
META_WEIGHT: '%' NUM;
TERMINAL: ~[<\n\r#%'|]+;
END_QUOTE: '`' -> mode(DEFAULT_MODE);
EMPTY_LINE: NL [ \t]* NL -> mode(DEFAULT_MODE);
NEWLINE: NL;
OPEN_PIPE: '|' -> mode(DEFAULT_MODE);