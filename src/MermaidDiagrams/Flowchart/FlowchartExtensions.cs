namespace MermaidDiagrams.Flowchart;

public static class FlowchartExtensions
{
	public static T Link<T>(this T chart, INode a, INode b, Edge? edge = null)
		where T : FlowchartBase
	{
		chart.Add(new Link(a, (b, edge ?? Edge.Open)));
		return chart;
	}

	public static T Link<T>(this T chart, string a, string b, Edge? edge = null)
		where T : FlowchartBase => chart.Link(Flowchart.Node.Create(a), Flowchart.Node.Create(b), edge);
	
	public static T Link<T>(this T chart, INode a, string b, Edge? edge = null)
		where T : FlowchartBase => chart.Link(a, Flowchart.Node.Create(b), edge);
	
	public static T Link<T>(this T chart, string a, INode b, Edge? edge = null)
		where T : FlowchartBase => chart.Link(Flowchart.Node.Create(a), b, edge);
	
	public static T Links<T>(this T chart, INode anchor, IEnumerable<(INode Node, Edge Edge)> to)
		where T : FlowchartBase => chart.Add(new Link(anchor, to as (INode Node, Edge Edge)[] ?? to.ToArray()));
	
	public static T Links<T>(this T chart, IEnumerable<INode> nodes, Edge edge)
		where T : FlowchartBase
	{
		var enumerable = nodes as INode[] ?? nodes.ToArray();
		return chart.Add(new Link(enumerable.First(), enumerable.Skip(1).Select(x => (x, edge)).ToArray()));
	}

	public static T Invisible<T>(this T chart, string id)
		where T : FlowchartBase
	{
		var node = chart.AddAnd(Flowchart.Node.Create(id, " ", Shape.Box));

		// todo these need to only add one instance
		chart.Statements.Add(new ClassAssign(ClassDef.InvisibleName, node.Id));
		chart.Statements.Add(ClassDef.Invisible);

		return chart;
	}

	public static T Node<T>(this T chart)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(null, string.Empty, Shape.Box));

	public static T Node<T>(this T chart, string id)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(id, string.Empty, Shape.Box));

	public static T Node<T>(this T chart, Shape style)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, style));

	public static T Node<T>(this T chart, Text text, Shape style)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(Flowchart.Node.NextId(), text, style));

	public static T Node<T>(this T chart, string id, Text text, Shape style)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(id, text, style));
	
	public static Text Markdown(this string content) => new(content, true);
}