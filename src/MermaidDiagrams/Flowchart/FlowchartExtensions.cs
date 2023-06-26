using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public static class FlowchartExtensions
{
	public static T Link<T>(this T flow, Identifier a, Identifier b, Edge? edge = null)
		where T : FlowchartBase
	{
		flow.Add(new Link(a, (b, edge ?? Edge.Open)));
		return flow;
	}

	public static T Link<T>(this T flow, IIdentifiable a, IIdentifiable b, Edge? edge = null)
		where T : FlowchartBase => flow.Link(a.Id, b.Id, edge);

	public static T Link<T>(this T flow, IIdentifiable a, Identifier b, Edge? edge = null)
		where T : FlowchartBase => flow.Link(a.Id, b, edge);

	public static T Link<T>(this T flow, Identifier a, IIdentifiable b, Edge? edge = null)
		where T : FlowchartBase => flow.Link(a, b.Id, edge);

	public static T Links<T>(this T flow, IIdentifiable anchor, IEnumerable<(IIdentifiable Node, Edge Edge)> to)
		where T : FlowchartBase => Links(flow, anchor.Id, to.Select(x => (x.Node.Id, x.Edge)));

	public static T Links<T>(this T flow, Identifier anchor, IEnumerable<(Identifier Id, Edge Edge)> to)
		where T : FlowchartBase
	{
		var ids = to.ToArray();
		return flow.AddAnd(new Link(anchor, ids.Skip(1).Select(x => (x.Id, x.Edge)).ToArray()));
	}

	public static T Links<T>(this T flow, IEnumerable<IIdentifiable> nodes, Edge edge)
		where T : FlowchartBase
		=> Links(flow, nodes.Select(x => x.Id), edge);

	public static T Links<T>(this T flow, IEnumerable<Identifier> nodes, Edge edge)
		where T : FlowchartBase
	{
		var ids = nodes.ToArray();
		return flow.AddAnd(new Link(ids.First(), ids.Skip(1).Select(x => (x, edge)).ToArray()));
	}

	public static T NodeInvisible<T>(this T flow, Identifier id)
		where T : FlowchartBase
	{
		flow.CreateInvisible(id);
		return flow;
	}

	public static T Node<T>(this T flow)
		where T : FlowchartBase => flow.AddAnd(new Node(Flowchart.Node.NextId(), string.Empty, Shape.Box));

	public static T Node<T>(this T flow, Identifier id)
		where T : FlowchartBase => flow.AddAnd(new Node(id, string.Empty, Shape.Box));

	public static T Node<T>(this T flow, Shape style)
		where T : FlowchartBase => flow.AddAnd(new Node(Flowchart.Node.NextId(), string.Empty, style));

	public static T Node<T>(this T flow, Text text, Shape style)
		where T : FlowchartBase => flow.AddAnd(new Node(Flowchart.Node.NextId(), text, style));

	public static T Node<T>(this T flow, Identifier id, Text text, Shape style)
		where T : FlowchartBase => flow.AddAnd(new Node(id, text, style));

	public static T Subgraph<T>(
		this T flow, Text label, Identifier? id = null, FlowDirection? direction = null,
		Action<FlowchartSubgraph>? builder = null
	)
		where T : FlowchartBase
	{
		flow.CreateSubgraph(label, id, direction, builder);
		return flow;
	}

	public static Text Markdown(this string content) => new(content, true);
}