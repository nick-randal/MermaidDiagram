namespace MermaidDiagrams.Flowchart;

public static class FlowchartExtensions
{
	public static T Link<T>(this T chart, INode a, INode b, Edge? edge = null)
		where T : FlowchartBase
	{
		chart.Add(new Link(a, b, edge ?? Edge.Open));
		return chart;
	}

	public static T Link<T>(this T chart, string a, string b, Edge? edge = null)
		where T : FlowchartBase => chart.Link(Node.Create(a), Node.Create(b), edge);
	
	public static T Link<T>(this T chart, INode a, string b, Edge? edge = null)
		where T : FlowchartBase => chart.Link(a, Node.Create(b), edge);
	
	public static T Link<T>(this T chart, string a, INode b, Edge? edge = null)
		where T : FlowchartBase => chart.Link(Node.Create(a), b, edge);
	
	/*public static T Links<T>(this T chart, params INode[] nodes)
		where T : FlowchartBase => */

	public static T AddNode<T>(this T chart)
		where T : FlowchartBase => chart.Add(Node.Create(null, string.Empty, Shape.Box));

	public static T AddNode<T>(this T chart, string id)
		where T : FlowchartBase => chart.Add(Node.Create(id, string.Empty, Shape.Box));

	public static T AddNode<T>(this T chart, Shape style)
		where T : FlowchartBase => chart.Add(Node.Create(Node.NextId(), string.Empty, style));

	public static T AddNode<T>(this T chart, Text text, Shape style)
		where T : FlowchartBase => chart.Add(Node.Create(Node.NextId(), text, style));

	public static T AddNode<T>(this T chart, string id, Text text, Shape style)
		where T : FlowchartBase => chart.Add(Node.Create(id, text, style));
	
	public static Text Markdown(this string content) => new(content, true);
}