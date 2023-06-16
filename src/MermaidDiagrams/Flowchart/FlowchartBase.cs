namespace MermaidDiagrams.Flowchart;

public abstract class FlowchartBase : DiagramBase
{
	public INode Node() => this.AddAnd(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, Shape.Box));

	public INode Node(string id) => this.AddAnd(Flowchart.Node.Create(id, string.Empty, Shape.Box));

	public INode Node(Shape style) => this.AddAnd(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, style));

	public INode Node(Text text, Shape style) => this.AddAnd(Flowchart.Node.Create(Flowchart.Node.NextId(), text, style));

	public INode Node(string id, Text text, Shape style) => this.AddAnd(Flowchart.Node.Create(id, text, style));

	public INode this[Identifier id]
		=> Renderables.Single(s => s is INode node && node.Id.Equals(id)) as INode
			?? throw new KeyNotFoundException($"Node not found for id: {id}");
}