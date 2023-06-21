using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public static class DiagramBaseExtensions
{
	public static T AddAnd<T>(this T chart, params IStatement[] statements)
		where T : DiagramBase
	{
		if (statements.Length == 0)
			return chart;

		chart.AddRenderables(statements);
		return chart;
	}
	
	public static T Add<T>(this DiagramBase chart, T statement) where T : IStatement
	{
		chart.AddRenderables(statement);
		return statement;
	}
}