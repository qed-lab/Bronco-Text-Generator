parser grammar BroncoParser;

options { tokenVocab=BroncoLexer; }

file: bag+;
bag: bag_title bag_line+ BAG_CLOSE?;
bag_line: (TERMINAL | NON_TERMINAL)+LINE_CLOSE?;
bag_title: TITLE_OPEN TITLE_NAME TITLE_CLOSE;
