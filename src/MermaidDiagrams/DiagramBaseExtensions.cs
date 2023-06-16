using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public static class DiagramBaseExtensions
{
	public static T Add<T>(this T chart, params IStatement[] statements)
		where T : DiagramBase
	{
		chart.Statements.AddRange(statements);
		return chart;
	}
}