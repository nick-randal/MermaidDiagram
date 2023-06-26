using MermaidDiagrams.Contracts;
using MermaidDiagrams.Support;

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

		_nodeLookup = new Dictionary<Identifier, INode>();
	}

	internal override void OnItemChange(IRenderable item, ItemChange change)
	{
		if (item is INode node)
		{
			if (change == ItemChange.Added)
				_nodeLookup.Add(node.Id, node);
			else
				_nodeLookup.Remove(node.Id);
		}
	}

	public INode CreateNode() => Add(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, Shape.Box));

	public INode CreateNode(string id) => Add(Flowchart.Node.Create(id, string.Empty, Shape.Box));

	public INode CreateNode(Shape style) => Add(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, style));

	public INode CreateNode(Text text, Shape style) => Add(Flowchart.Node.Create(Flowchart.Node.NextId(), text, style));

	public INode CreateNode(string id, Text text, Shape style) => Add(Flowchart.Node.Create(id, text, style));

	public new INode this[Identifier id] => _nodeLookup[id];

	public INode CreateInvisible(Identifier id)
	{
		var node = Add(Flowchart.Node.CreateInvisible(id));
		var def = GetClassDefinitions().GetOrCreate(ClassDef.InvisibleName, _ => ClassDef.Invisible);
		def.Assign(node);
		return node;
	}

	public ILink CreateLink(ILink link) => Add(link);

	public ILink CreateLink(IIdentifiable a, IIdentifiable b, Edge? edge = null) 
		=> CreateLink(a.Id, b.Id, edge ?? Edge.Open);

	public ILink CreateLink(Identifier a, Identifier b, Edge? edge = null)
		=> Add(new Link(a, (b, edge ?? Edge.Open)));

	public ILink CreateLink(IIdentifiable a, Identifier b, Edge? edge = null)
		=>CreateLink(a.Id, b, edge);

	public ILink CreateLink(Identifier a, IIdentifiable b, Edge? edge = null)
		=> CreateLink(a, b.Id, edge);

	public ILink CreateLinks(INode anchor, IEnumerable<(IIdentifiable Node, Edge Edge)> to)
		=> Add(new Link(anchor.Id, to.Select(x => (x.Node.Id, x.Edge)).ToArray()));

	public ILink CreateLinks(IEnumerable<IIdentifiable> nodes, Edge edge)
	{
		var enumerable = (nodes as INode[] ?? nodes.ToArray());
		return Add(new Link(enumerable.First().Id, enumerable.Skip(1).Select(x => (x.Id, edge)).ToArray()));
	}
	
	public ILink CreateLinks(IEnumerable<Identifier> nodes, Edge edge)
	{
		var ids = nodes.ToArray();
		return Add(new Link(ids.First(), ids.Skip(1).Select(x => (x, edge)).ToArray()));
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
	private readonly Dictionary<Identifier,INode> _nodeLookup;
}