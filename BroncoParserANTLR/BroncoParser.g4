parser grammar BroncoParser;

options {   tokenVocab = BroncoLexer; }

/*
PARSER
*/

file: bag+ EOF;

bag: bag_title bag_item+;
bag_title: TITLE bag_title_args? TITLE_NEWLINE;
bag_title_args: TITLE_COLON TITLE_ID (TITLE_COMMA TITLE_ID)*;
bag_item: symbol (PIPE symbol_ref)? NEWLINE?;
symbol: symbol_list_item+ meta_data*;
symbol_list_item: TERMINAL | symbol_insert;

symbol_insert: LT (symbol_ref | symbol_call_inner) GT;

symbol_ref: IDENTIFIER | symbol_call;
symbol_call: OP symbol_call_inner CP;
symbol_call_inner: IDENTIFIER symbol_call_args?;
symbol_call_args: COLON symbol_ref (COMMA symbol_ref)*;

meta_data: META_TAG | META_WEIGHT;
