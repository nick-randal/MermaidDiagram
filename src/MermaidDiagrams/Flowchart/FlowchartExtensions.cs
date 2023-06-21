using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public static class FlowchartExtensions
{
	public static T LinkAnd<T>(this T chart, IIdentifiable a, IIdentifiable b, Edge? edge = null)
		where T : FlowchartBase
	{
		chart.Add(new Link(a, (b, edge ?? Edge.Open)));
		return chart;
	}

	public static T LinkAnd<T>(this T chart, string a, string b, Edge? edge = null)
		where T : FlowchartBase => chart.LinkAnd((NodeId)a, (NodeId)b, edge);
	
	public static T LinkAnd<T>(this T chart, IIdentifiable a, string b, Edge? edge = null)
		where T : FlowchartBase => chart.LinkAnd(a, (NodeId)b, edge);
	
	public static T LinkAnd<T>(this T chart, string a, IIdentifiable b, Edge? edge = null)
		where T : FlowchartBase => chart.LinkAnd((NodeId)a, b, edge);
	
	public static T LinksAnd<T>(this T chart, INode anchor, IEnumerable<(IIdentifiable Node, Edge Edge)> to)
		where T : FlowchartBase => chart.AddAnd(new Link(anchor, to as (IIdentifiable Node, Edge Edge)[] ?? to.ToArray()));
	
	public static T LinksAnd<T>(this T chart, IEnumerable<IIdentifiable> nodes, Edge edge)
		where T : FlowchartBase
	{
		var enumerable = nodes as INode[] ?? nodes.ToArray();
		return chart.AddAnd(new Link(enumerable.First(), enumerable.Skip(1).Select(x => (x, edge)).ToArray()));
	}

	public static T InvisibleAnd<T>(this T chart, string id)
		where T : FlowchartBase
	{
		chart.Invisible(id);
		return chart;
	}

	public static T NodeAnd<T>(this T chart)
		where T : FlowchartBase => chart.AddAnd(Node.Create(null, string.Empty, Shape.Box));

	public static T NodeAnd<T>(this T chart, string id)
		where T : FlowchartBase => chart.AddAnd(Node.Create(id, string.Empty, Shape.Box));

	public static T NodeAnd<T>(this T chart, Shape style)
		where T : FlowchartBase => chart.AddAnd(Node.Create(Node.NextId(), string.Empty, style));

	public static T NodeAnd<T>(this T chart, Text text, Shape style)
		where T : FlowchartBase => chart.AddAnd(Node.Create(Node.NextId(), text, style));

	public static T NodeAnd<T>(this T chart, string id, Text text, Shape style)
		where T : FlowchartBase => chart.AddAnd(Node.Create(id, text, style));

	public static Text Markdown(this string content) => new(content, true);
}