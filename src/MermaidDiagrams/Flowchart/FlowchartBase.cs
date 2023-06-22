using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public abstract class FlowchartBase : MermaidBase, IStatement
{
	protected FlowchartBase(FlowchartBase? parent, IDiagramType type) : base(type)
	{
		Parent = parent;
		_lazyRoot = new Lazy<FlowchartBase>(() =>
		{
			var current = this;
			while(current.Parent is not null)
				current = current.Parent;

			return current;
		});
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
		var def = GetClassDefinitions().GetOrCreate(ClassDef.InvisibleName, _ => ClassDef.Invisible);
		def.Assign(node);
		return node;
	}

	public ILink CreateLink(ILink link) => Add(link);

	public ILink CreateLink(IIdentifiable a, IIdentifiable b, Edge? edge = null) 
		=> Add( new Link(a, (b, edge ?? Edge.Open)));

	public ILink CreateLink(string a, string b, Edge? edge = null)
		=> CreateLink((NodeId)a, (NodeId)b, edge);

	public ILink CreateLink(IIdentifiable a, string b, Edge? edge = null)
		=>CreateLink(a, (NodeId)b, edge);

	public ILink CreateLink(string a, IIdentifiable b, Edge? edge = null)
		=> CreateLink((NodeId)a, b, edge);

	public ILink CreateLinks(INode anchor, IEnumerable<(IIdentifiable Node, Edge Edge)> to)
		=> Add(new Link(anchor, to as (IIdentifiable Node, Edge Edge)[] ?? to.ToArray()));

	public ILink CreateLinks(IEnumerable<IIdentifiable> nodes, Edge edge)
	{
		var enumerable = (nodes as INode[] ?? nodes.ToArray());
		return Add(new Link(enumerable.First(), enumerable.Skip(1).Select(x => (x, edge)).ToArray()));
	}

	public FlowchartSubgraph CreateSubgraph(Text label, Identifier? id = null, FlowDirection? direction = null, Action<FlowchartSubgraph>? builder = null)
	{
		var subgraph = new FlowchartSubgraph(Parent ?? this, label, id);
		if (direction is not null)
			subgraph.SetDirection(direction.Value);
		
		builder?.Invoke(subgraph);

		Add(subgraph);
		return subgraph;
	}
	
	public ClassDefinitions GetClassDefinitions() => _lazyRoot.Value.GetOrCreate<ClassDefinitions>();

	protected FlowchartBase? Parent { get; }
	
	private readonly Lazy<FlowchartBase> _lazyRoot;
}