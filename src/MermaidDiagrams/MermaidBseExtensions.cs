using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public static class MermaidBseExtensions
{
	public static T AddAnd<T>(this T mermaid, params IStatement[] statements)
		where T : MermaidBase
	{
		if (statements.Length == 0)
			return mermaid;

		mermaid.AddRange(statements);
		return mermaid;
	}
	
	public static T Add<T>(this MermaidBase mermaid, T statement) where T : IStatement
	{
		mermaid.Add(statement);
		return statement;
	}
	
	public static T Comment<T>(this T mermaid, string comment)
		where T : MermaidBase
	{
		mermaid.Add(new Comment(comment));
		return mermaid;
	}
	
	public static T Literal<T>(this T mermaid, string literal)
		where T : MermaidBase
	{
		mermaid.Add(new Literal(literal));
		return mermaid;
	}
}