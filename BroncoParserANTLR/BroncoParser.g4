parser grammar BroncoParser;

options { tokenVocab=BroncoLexer; }

file: bag+;
bag: bag_title bag_line+ BAG_CLOSE?;
bag_line: (TERMINAL | non_terminal)+LINE_CLOSE?;
non_terminal: NON_TERMINAL_OPEN TARGET non_terminal_args? NON_TERMINAL_CLOSE;
non_terminal_args:ARG_START TARGET (ARG_SEPERATE TARGET)*;
bag_title: TITLE_OPEN TITLE_NAME TITLE_CLOSE;
