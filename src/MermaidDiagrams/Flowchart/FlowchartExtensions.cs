using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public static class FlowchartExtensions
{
	public static T Add<T>(this T chart, params IStatement[] statements) // todo move to DiagramBaseExtensions
		where T : DiagramBase
	{
		chart.Statements.AddRange(statements);
		return chart;
	}

	public static T Link<T>(this T chart, INode a, INode b, Edge edge, Text label)
		where T : FlowchartBase
	{
		chart.Add(new Link(a, b, edge, label));
		return chart;
	}

	public static T Link<T>(this T chart, string a, string b, Edge edge, Text label)
		where T : FlowchartBase => chart.Link(Flowchart.Node.Create(a), Flowchart.Node.Create(b), edge, label);

	public static T Link<T>(this T chart, INode a, INode b, Edge edge)
		where T : FlowchartBase => chart.Link(a, b, edge, Text.Empty);

	public static T Link<T>(this T chart, string a, string b, Edge edge)
		where T : FlowchartBase => chart.Link(Flowchart.Node.Create(a), Flowchart.Node.Create(b), edge, Text.Empty);

	public static T Link<T>(this T chart, INode a, INode b, Text label)
		where T : FlowchartBase => chart.Link(a, b, Edge.Open, label);

	public static T AddNode<T>(this T chart)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(null, string.Empty, Shape.Box));

	public static T AddNode<T>(this T chart, string id)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(id, string.Empty, Shape.Box));

	public static T AddNode<T>(this T chart, Shape style)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, style));

	public static T AddNode<T>(this T chart, Text text, Shape style)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(Flowchart.Node.NextId(), text, style));

	public static T AddNode<T>(this T chart, string id, Text text, Shape style)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(id, text, style));
	
	public static Text Markdown(this string content) => new(content, true);
}