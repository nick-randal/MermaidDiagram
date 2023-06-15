using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

using This = FlowchartGraph;

public class FlowchartGraph : DiagramBase<FlowchartGraph, IFlowchartOptions>
{
	public FlowchartGraph(FlowDirection direction)
	{
		SetType((TypeOfFlowchart)direction);
	}

	public override FlowchartGraph Add(params IStatement[] statements)
	{
		Statements.AddRange(statements);
		return this;
	}

	public This Link(INode a, INode b, Edge edge, Text label)
	{
		Statements.Add(new Link(a, b, edge, label));
		return this;
	}
	
	public This Link(string a, string b, Edge edge, Text label) => Link(Flowchart.Node.Create(a), Flowchart.Node.Create(b), edge, label);

	public This AddNode() => Add(Flowchart.Node.Create(NextId, string.Empty, Shape.Box));

	public This AddNode(string id) => Add(Flowchart.Node.Create(id, string.Empty, Shape.Box));

	public This AddNode(Shape style) => Add(Flowchart.Node.Create(NextId, string.Empty, style));

	public This AddNode(Text text, Shape style) => Add(Flowchart.Node.Create(NextId, text, style));

	public This AddNode(string id, Text text, Shape style) => Add(Flowchart.Node.Create(id, text, style));

	public INode Node() => AddAnd(Flowchart.Node.Create(NextId, string.Empty, Shape.Box));

	public INode Node(string id) => AddAnd(Flowchart.Node.Create(id, string.Empty, Shape.Box));

	public INode Node(Shape style) => AddAnd(Flowchart.Node.Create(NextId, string.Empty, style));

	public INode Node(Text text, Shape style) => AddAnd(Flowchart.Node.Create(NextId, text, style));

	public INode Node(string id, Text text, Shape style) => AddAnd(Flowchart.Node.Create(id, text, style));

	public INode this[string id]
		=> Statements.Single(s => s is INode node && node.Id.Equals(id, StringComparison.OrdinalIgnoreCase)) as INode
			?? throw new KeyNotFoundException($"Node not found for id: {id}");

	public virtual string Render() => Render(new FlowchartOptions());

	public override string Render(IFlowchartOptions options)
	{
		var state = new RenderState<IFlowchartOptions>(options);

		var builder = new TextBuilder();

		base.Render(builder, state);

		// todo add option to have terse rendering if a node is referenced in an edge, it can be omitted from the node list
		// this would also change edge rendering to use the node id instead of the node text
		// if verbose is false, do a node lookup, if it is there then render it

		using var stepper = state.StepIn();
		foreach (var statement in Statements.Where(s => s is not ISpecialStatement))
		{
			if(statement is not IComment)
				builder.Append(state.Indent);
			statement.Render(builder, state);
		}

		return builder.Text;
	}

	private string NextId => $"id{Interlocked.Increment(ref _idCounter)}";

	private long _idCounter = 0;
}