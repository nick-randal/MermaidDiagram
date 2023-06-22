using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public static class FlowchartExtensions
{
	public static T Link<T>(this T flow, IIdentifiable a, IIdentifiable b, Edge? edge = null)
		where T : FlowchartBase
	{
		flow.Add(new Link(a, (b, edge ?? Edge.Open)));
		return flow;
	}

	public static T Link<T>(this T flow, string a, string b, Edge? edge = null)
		where T : FlowchartBase => flow.Link((NodeId)a, (NodeId)b, edge);
	
	public static T Link<T>(this T flow, IIdentifiable a, string b, Edge? edge = null)
		where T : FlowchartBase => flow.Link(a, (NodeId)b, edge);
	
	public static T Link<T>(this T flow, string a, IIdentifiable b, Edge? edge = null)
		where T : FlowchartBase => flow.Link((NodeId)a, b, edge);
	
	public static T Links<T>(this T flow, INode anchor, IEnumerable<(IIdentifiable Node, Edge Edge)> to)
		where T : FlowchartBase => flow.AddAnd(new Link(anchor, to as (IIdentifiable Node, Edge Edge)[] ?? to.ToArray()));
	
	public static T Links<T>(this T flow, IEnumerable<IIdentifiable> nodes, Edge edge)
		where T : FlowchartBase
	{
		var enumerable = nodes as INode[] ?? nodes.ToArray();
		return flow.AddAnd(new Link(enumerable.First(), enumerable.Skip(1).Select(x => (x, edge)).ToArray()));
	}

	public static T NodeInvisible<T>(this T flow, string id)
		where T : FlowchartBase
	{
		flow.Invisible(id);
		return flow;
	}

	public static T Node<T>(this T flow)
		where T : FlowchartBase => flow.AddAnd(Flowchart.Node.Create(null, string.Empty, Shape.Box));

	public static T Node<T>(this T flow, string id)
		where T : FlowchartBase => flow.AddAnd(Flowchart.Node.Create(id, string.Empty, Shape.Box));

	public static T Node<T>(this T flow, Shape style)
		where T : FlowchartBase => flow.AddAnd(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, style));

	public static T Node<T>(this T flow, Text text, Shape style)
		where T : FlowchartBase => flow.AddAnd(Flowchart.Node.Create(Flowchart.Node.NextId(), text, style));

	public static T Node<T>(this T flow, string id, Text text, Shape style)
		where T : FlowchartBase => flow.AddAnd(Flowchart.Node.Create(id, text, style));
	
	public static T Subgraph<T>(this T flow, Text label, Identifier? id = null, FlowDirection? direction = null, Action<FlowchartSubgraph>? builder = null)
		where T : FlowchartBase
	{
		flow.CreateSubgraph(label, id, direction, builder);
		return flow;
	}

	public static Text Markdown(this string content) => new(content, true);
}