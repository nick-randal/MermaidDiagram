namespace MermaidDiagrams.Flowchart;

public abstract class FlowchartBase : DiagramBase
{
	public INode Node() => this.Add(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, Shape.Box));

	public INode Node(string id) => this.Add(Flowchart.Node.Create(id, string.Empty, Shape.Box));

	public INode Node(Shape style) => this.Add(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, style));

	public INode Node(Text text, Shape style) => this.Add(Flowchart.Node.Create(Flowchart.Node.NextId(), text, style));

	public INode Node(string id, Text text, Shape style) => this.Add(Flowchart.Node.Create(id, text, style));

	public INode this[Identifier id] => GetRenderableOrThrow<INode>(id);

	public INode Invisible(Identifier id)
	{
		var node = this.Add(Flowchart.Node.CreateInvisible(id));
		var def = GetClassDefinitions().GetOrCreate(ClassDef.InvisibleName, _ => ClassDef.Invisible);
		def.Assign(node);
		return node;
	}
	
	public FlowchartSubgraph Subgraph(Text label, Identifier? id = null, FlowDirection? direction = null, Action<FlowchartSubgraph>? builder = null)
	{
		var subgraph = new FlowchartSubgraph(label, id);
		if (direction is not null)
			subgraph.SetDirection(direction.Value);
		
		builder?.Invoke(subgraph);

		this.Add(subgraph);
		return subgraph;
	}
}