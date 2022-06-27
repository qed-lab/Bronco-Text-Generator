//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.10.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:/Users/angry/source/repos/BroncoLibrary/ANTLRParser\BroncoParser.g4 by ANTLR 4.10.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="BroncoParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.10.1")]
[System.CLSCompliant(false)]
public interface IBroncoParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFile([NotNull] BroncoParser.FileContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.bag"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBag([NotNull] BroncoParser.BagContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.bag_title"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBag_title([NotNull] BroncoParser.Bag_titleContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.bag_title_args"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBag_title_args([NotNull] BroncoParser.Bag_title_argsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.bag_item"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBag_item([NotNull] BroncoParser.Bag_itemContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.symbol"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSymbol([NotNull] BroncoParser.SymbolContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.symbol_list_item"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSymbol_list_item([NotNull] BroncoParser.Symbol_list_itemContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.symbol_insert"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSymbol_insert([NotNull] BroncoParser.Symbol_insertContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.symbol_ref"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSymbol_ref([NotNull] BroncoParser.Symbol_refContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.symbol_call"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSymbol_call([NotNull] BroncoParser.Symbol_callContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.symbol_call_inner"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSymbol_call_inner([NotNull] BroncoParser.Symbol_call_innerContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.symbol_call_args"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSymbol_call_args([NotNull] BroncoParser.Symbol_call_argsContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="BroncoParser.meta_data"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMeta_data([NotNull] BroncoParser.Meta_dataContext context);
}