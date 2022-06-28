//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.10.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/angry/source/repos/BroncoLibrary/ANTLRParser\BroncoLexer.g4 by ANTLR 4.10.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.10.1")]
[System.CLSCompliant(false)]
public partial class BroncoLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		TITLE=1, BOOL_LITERAL=2, IDENTIFIER=3, NUMBER=4, SKIP_COMMENT=5, SKIP_WS=6, 
		COLON=7, COMMA=8, OP=9, CP=10, GT=11, START_QUOTE=12, CLOSE_PIPE=13, TITLE_COLON=14, 
		TITLE_COMMA=15, TITLE_ID=16, TITLE_SKIP_WS=17, TITLE_NEWLINE=18, LT=19, 
		META_TAG=20, META_WEIGHT=21, TERMINAL=22, END_QUOTE=23, EMPTY_LINE=24, 
		NEWLINE=25, OPEN_PIPE=26;
	public const int
		TITLE_MODE=1, TERMINAL_MODE=2;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE", "TITLE_MODE", "TERMINAL_MODE"
	};

	public static readonly string[] ruleNames = {
		"ID", "NUM", "NL", "TITLE", "BOOL_LITERAL", "IDENTIFIER", "NUMBER", "SKIP_COMMENT", 
		"SKIP_WS", "COLON", "COMMA", "OP", "CP", "GT", "START_QUOTE", "CLOSE_PIPE", 
		"TITLE_COLON", "TITLE_COMMA", "TITLE_ID", "TITLE_SKIP_WS", "TITLE_NEWLINE", 
		"LT", "META_TAG", "META_WEIGHT", "TERMINAL", "END_QUOTE", "EMPTY_LINE", 
		"NEWLINE", "OPEN_PIPE"
	};


	public BroncoLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public BroncoLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, null, null, "':'", "','", "'('", "')'", 
		"'>'", null, null, null, null, null, null, null, "'<'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "TITLE", "BOOL_LITERAL", "IDENTIFIER", "NUMBER", "SKIP_COMMENT", 
		"SKIP_WS", "COLON", "COMMA", "OP", "CP", "GT", "START_QUOTE", "CLOSE_PIPE", 
		"TITLE_COLON", "TITLE_COMMA", "TITLE_ID", "TITLE_SKIP_WS", "TITLE_NEWLINE", 
		"LT", "META_TAG", "META_WEIGHT", "TERMINAL", "END_QUOTE", "EMPTY_LINE", 
		"NEWLINE", "OPEN_PIPE"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "BroncoLexer.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override int[] SerializedAtn { get { return _serializedATN; } }

	static BroncoLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static int[] _serializedATN = {
		4,0,26,206,6,-1,6,-1,6,-1,2,0,7,0,2,1,7,1,2,2,7,2,2,3,7,3,2,4,7,4,2,5,
		7,5,2,6,7,6,2,7,7,7,2,8,7,8,2,9,7,9,2,10,7,10,2,11,7,11,2,12,7,12,2,13,
		7,13,2,14,7,14,2,15,7,15,2,16,7,16,2,17,7,17,2,18,7,18,2,19,7,19,2,20,
		7,20,2,21,7,21,2,22,7,22,2,23,7,23,2,24,7,24,2,25,7,25,2,26,7,26,2,27,
		7,27,2,28,7,28,1,0,1,0,5,0,64,8,0,10,0,12,0,67,9,0,1,1,3,1,70,8,1,1,1,
		4,1,73,8,1,11,1,12,1,74,1,1,1,1,4,1,79,8,1,11,1,12,1,80,3,1,83,8,1,1,2,
		3,2,86,8,2,1,2,1,2,1,3,1,3,1,3,1,3,1,3,1,4,1,4,1,4,1,4,1,4,1,4,1,4,1,4,
		1,4,3,4,104,8,4,1,5,1,5,1,6,1,6,1,7,1,7,1,7,1,7,4,7,114,8,7,11,7,12,7,
		115,1,7,1,7,1,7,1,7,1,7,1,8,4,8,124,8,8,11,8,12,8,125,1,8,1,8,1,9,1,9,
		1,10,1,10,1,11,1,11,1,12,1,12,1,13,1,13,1,13,1,13,1,14,1,14,1,14,1,14,
		1,15,1,15,1,15,1,15,1,16,1,16,1,17,1,17,1,18,1,18,1,19,4,19,157,8,19,11,
		19,12,19,158,1,19,1,19,1,20,1,20,1,20,1,20,1,21,1,21,1,21,1,21,1,22,1,
		22,1,22,1,22,1,22,3,22,176,8,22,1,23,1,23,1,23,1,24,4,24,182,8,24,11,24,
		12,24,183,1,25,1,25,1,25,1,25,1,26,1,26,5,26,192,8,26,10,26,12,26,195,
		9,26,1,26,1,26,1,26,1,26,1,27,1,27,1,28,1,28,1,28,1,28,0,0,29,3,0,5,0,
		7,0,9,1,11,2,13,3,15,4,17,5,19,6,21,7,23,8,25,9,27,10,29,11,31,12,33,13,
		35,14,37,15,39,16,41,17,43,18,45,19,47,20,49,21,51,22,53,23,55,24,57,25,
		59,26,3,0,1,2,7,2,0,65,90,97,122,4,0,48,57,65,90,95,95,97,122,1,0,48,57,
		1,0,42,42,3,0,9,10,13,13,32,32,2,0,9,9,32,32,7,0,10,10,13,13,35,35,37,
		37,60,60,96,96,124,124,213,0,9,1,0,0,0,0,11,1,0,0,0,0,13,1,0,0,0,0,15,
		1,0,0,0,0,17,1,0,0,0,0,19,1,0,0,0,0,21,1,0,0,0,0,23,1,0,0,0,0,25,1,0,0,
		0,0,27,1,0,0,0,0,29,1,0,0,0,0,31,1,0,0,0,0,33,1,0,0,0,1,35,1,0,0,0,1,37,
		1,0,0,0,1,39,1,0,0,0,1,41,1,0,0,0,1,43,1,0,0,0,2,45,1,0,0,0,2,47,1,0,0,
		0,2,49,1,0,0,0,2,51,1,0,0,0,2,53,1,0,0,0,2,55,1,0,0,0,2,57,1,0,0,0,2,59,
		1,0,0,0,3,61,1,0,0,0,5,69,1,0,0,0,7,85,1,0,0,0,9,89,1,0,0,0,11,103,1,0,
		0,0,13,105,1,0,0,0,15,107,1,0,0,0,17,109,1,0,0,0,19,123,1,0,0,0,21,129,
		1,0,0,0,23,131,1,0,0,0,25,133,1,0,0,0,27,135,1,0,0,0,29,137,1,0,0,0,31,
		141,1,0,0,0,33,145,1,0,0,0,35,149,1,0,0,0,37,151,1,0,0,0,39,153,1,0,0,
		0,41,156,1,0,0,0,43,162,1,0,0,0,45,166,1,0,0,0,47,170,1,0,0,0,49,177,1,
		0,0,0,51,181,1,0,0,0,53,185,1,0,0,0,55,189,1,0,0,0,57,200,1,0,0,0,59,202,
		1,0,0,0,61,65,7,0,0,0,62,64,7,1,0,0,63,62,1,0,0,0,64,67,1,0,0,0,65,63,
		1,0,0,0,65,66,1,0,0,0,66,4,1,0,0,0,67,65,1,0,0,0,68,70,5,45,0,0,69,68,
		1,0,0,0,69,70,1,0,0,0,70,72,1,0,0,0,71,73,7,2,0,0,72,71,1,0,0,0,73,74,
		1,0,0,0,74,72,1,0,0,0,74,75,1,0,0,0,75,82,1,0,0,0,76,78,5,46,0,0,77,79,
		7,2,0,0,78,77,1,0,0,0,79,80,1,0,0,0,80,78,1,0,0,0,80,81,1,0,0,0,81,83,
		1,0,0,0,82,76,1,0,0,0,82,83,1,0,0,0,83,6,1,0,0,0,84,86,5,13,0,0,85,84,
		1,0,0,0,85,86,1,0,0,0,86,87,1,0,0,0,87,88,5,10,0,0,88,8,1,0,0,0,89,90,
		5,64,0,0,90,91,3,3,0,0,91,92,1,0,0,0,92,93,6,3,0,0,93,10,1,0,0,0,94,95,
		5,116,0,0,95,96,5,114,0,0,96,97,5,117,0,0,97,104,5,101,0,0,98,99,5,102,
		0,0,99,100,5,97,0,0,100,101,5,108,0,0,101,102,5,115,0,0,102,104,5,101,
		0,0,103,94,1,0,0,0,103,98,1,0,0,0,104,12,1,0,0,0,105,106,3,3,0,0,106,14,
		1,0,0,0,107,108,3,5,1,0,108,16,1,0,0,0,109,110,5,47,0,0,110,111,5,42,0,
		0,111,113,1,0,0,0,112,114,8,3,0,0,113,112,1,0,0,0,114,115,1,0,0,0,115,
		113,1,0,0,0,115,116,1,0,0,0,116,117,1,0,0,0,117,118,5,42,0,0,118,119,5,
		47,0,0,119,120,1,0,0,0,120,121,6,7,1,0,121,18,1,0,0,0,122,124,7,4,0,0,
		123,122,1,0,0,0,124,125,1,0,0,0,125,123,1,0,0,0,125,126,1,0,0,0,126,127,
		1,0,0,0,127,128,6,8,1,0,128,20,1,0,0,0,129,130,5,58,0,0,130,22,1,0,0,0,
		131,132,5,44,0,0,132,24,1,0,0,0,133,134,5,40,0,0,134,26,1,0,0,0,135,136,
		5,41,0,0,136,28,1,0,0,0,137,138,5,62,0,0,138,139,1,0,0,0,139,140,6,13,
		2,0,140,30,1,0,0,0,141,142,5,96,0,0,142,143,1,0,0,0,143,144,6,14,2,0,144,
		32,1,0,0,0,145,146,5,124,0,0,146,147,1,0,0,0,147,148,6,15,2,0,148,34,1,
		0,0,0,149,150,3,21,9,0,150,36,1,0,0,0,151,152,3,23,10,0,152,38,1,0,0,0,
		153,154,3,3,0,0,154,40,1,0,0,0,155,157,7,5,0,0,156,155,1,0,0,0,157,158,
		1,0,0,0,158,156,1,0,0,0,158,159,1,0,0,0,159,160,1,0,0,0,160,161,6,19,1,
		0,161,42,1,0,0,0,162,163,3,7,2,0,163,164,1,0,0,0,164,165,6,20,2,0,165,
		44,1,0,0,0,166,167,5,60,0,0,167,168,1,0,0,0,168,169,6,21,3,0,169,46,1,
		0,0,0,170,171,5,35,0,0,171,175,3,3,0,0,172,173,3,21,9,0,173,174,3,5,1,
		0,174,176,1,0,0,0,175,172,1,0,0,0,175,176,1,0,0,0,176,48,1,0,0,0,177,178,
		5,37,0,0,178,179,3,5,1,0,179,50,1,0,0,0,180,182,8,6,0,0,181,180,1,0,0,
		0,182,183,1,0,0,0,183,181,1,0,0,0,183,184,1,0,0,0,184,52,1,0,0,0,185,186,
		5,96,0,0,186,187,1,0,0,0,187,188,6,25,3,0,188,54,1,0,0,0,189,193,3,7,2,
		0,190,192,7,5,0,0,191,190,1,0,0,0,192,195,1,0,0,0,193,191,1,0,0,0,193,
		194,1,0,0,0,194,196,1,0,0,0,195,193,1,0,0,0,196,197,3,7,2,0,197,198,1,
		0,0,0,198,199,6,26,3,0,199,56,1,0,0,0,200,201,3,7,2,0,201,58,1,0,0,0,202,
		203,5,124,0,0,203,204,1,0,0,0,204,205,6,28,3,0,205,60,1,0,0,0,16,0,1,2,
		65,69,74,80,82,85,103,115,125,158,175,183,193,4,2,1,0,6,0,0,2,2,0,2,0,
		0
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
