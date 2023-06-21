using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public abstract class FlowchartBase : MermaidBase
{
	protected FlowchartBase(FlowchartBase? parent, IDiagramType type) : base(type)
	{
		Parent = parent;
	}

	public INode Node() => Add(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, Shape.Box));

	public INode Node(string id) => Add(Flowchart.Node.Create(id, string.Empty, Shape.Box));

	public INode Node(Shape style) => Add(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, style));

	public INode Node(Text text, Shape style) => Add(Flowchart.Node.Create(Flowchart.Node.NextId(), text, style));

	public INode Node(string id, Text text, Shape style) => Add(Flowchart.Node.Create(id, text, style));

	public new INode this[Identifier id] => GetRenderableOrThrow<INode>(id);

	public INode Invisible(Identifier id)
	{
		var node = Add(Flowchart.Node.CreateInvisible(id));
		var def = GetOrCreate<ClassDefinitions>().GetOrCreate(ClassDef.InvisibleName, _ => ClassDef.Invisible);
		def.Assign(node);
		return node;
	}

	public void Link(ILink link) => Add(link);

	public ILink Link(IIdentifiable a, IIdentifiable b, Edge? edge = null) 
		=> Add( new Link(a, (b, edge ?? Edge.Open)));

	public ILink Link(string a, string b, Edge? edge = null)
		=> Link((NodeId)a, (NodeId)b, edge);

	public ILink Link(IIdentifiable a, string b, Edge? edge = null)
		=>Link(a, (NodeId)b, edge);

	public ILink Link(string a, IIdentifiable b, Edge? edge = null)
		=> Link((NodeId)a, b, edge);

	public ILink Links(INode anchor, IEnumerable<(IIdentifiable Node, Edge Edge)> to)
		=> Add(new Link(anchor, to as (IIdentifiable Node, Edge Edge)[] ?? to.ToArray()));

	public ILink Links<T>(IEnumerable<IIdentifiable> nodes, Edge edge)
		where T : FlowchartBase
	{
		var enumerable = (nodes as INode[] ?? nodes.ToArray());
		return Add(new Link(enumerable.First(), enumerable.Skip(1).Select(x => (x, edge)).ToArray()));
	}

	public FlowchartSubgraph Subgraph(Text label, Identifier? id = null, FlowDirection? direction = null, Action<FlowchartSubgraph>? builder = null)
	{
		var subgraph = new FlowchartSubgraph(Parent!, label, id);
		if (direction is not null)
			subgraph.SetDirection(direction.Value);
		
		builder?.Invoke(subgraph);

		Add(subgraph);
		return subgraph;
	}

	protected FlowchartBase? Parent { get; }
}