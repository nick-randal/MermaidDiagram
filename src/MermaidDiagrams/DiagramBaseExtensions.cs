using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public static class DiagramBaseExtensions
{
	public static T Add<T>(this T chart, params IStatement[] statements)
		where T : DiagramBase
	{
		if (statements.Length == 0)
			return chart;
		
		if (statements.Any(s => s is ISpecialStatement))
			throw new NotSupportedException(); // todo
		
		chart.Statements.AddRange(statements);
		return chart;
	}
	
	public static T AddAnd<T>(this DiagramBase chart, T statement) where T : IStatement
	{
		if (statement is ISpecialStatement)
			throw new NotSupportedException();
		
		chart.Statements.Add(statement);
		return statement;
	}
}