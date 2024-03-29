parser grammar BroncoParser;

options {   tokenVocab = BroncoLexer; }

/*
PARSER
*/

file: bag+ EOF;

bag: TITLE bag_title_args? bag_default_condition? TITLE_NEWLINE bag_item+ EMPTY_LINE?;
bag_default_condition: TITLE_OPEN_SQUARE symbol_ref CLOSE_SQUARE;
bag_title_args: TITLE_COLON TITLE_ID (TITLE_COMMA TITLE_ID)*;
bag_item: symbol (TERMINAL_OPEN_SQUARE symbol_ref CLOSE_SQUARE)? (NEWLINE | PIPE)?;
symbol: symbol_list_item+ meta_data*;
symbol_list_item: TERMINAL | symbol_insert;

symbol_insert: LT symbol_ref GT;

symbol_ref: symbol_call | OP symbol_ref CP | NUMBER | BOOL_LITERAL | (START_QUOTE symbol END_QUOTE)
          | symbol_ref EQUALS symbol_ref
          | symbol_ref QUESTION_MARK symbol_ref SEMI_COLON symbol_ref
          | DOLLAR symbol_ref;
symbol_call: IDENTIFIER symbol_call_args?;
symbol_call_args: COLON symbol_ref (COMMA symbol_ref)*;

meta_data: META_TAG | META_WEIGHT;