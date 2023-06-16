using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

using This = FlowchartGraph;

public class FlowchartGraph : DiagramBase<FlowchartGraph, IFlowchartOptions>
{
	public FlowchartGraph(FlowDirection direction)
	{
		SetType((TypeOfFlowchart)direction);
	}

	internal FlowchartGraph()
	{
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
	
	public This Link(INode a, INode b, Edge edge) => Link(a, b, edge, Text.Empty);
	
	public This Link(string a, string b, Edge edge) => Link(Flowchart.Node.Create(a), Flowchart.Node.Create(b), edge, Text.Empty);
	
	public This Link(INode a, INode b, Text label) => Link(a, b, Edge.Open, label);

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

		RenderStatements(builder, state);

		return builder.Text;
	}

	protected void RenderStatements(ITextBuilder builder, IRenderState state)
	{
		using var stepper = state.StepIn();
		foreach (var statement in Statements.Where(s => s is not ISpecialStatement))
		{
			if (statement is not IComment)
				builder.Append(state.Indent);
			statement.Render(builder, state);
		}
	}

	private string NextId => $"id{Interlocked.Increment(ref _idCounter)}";

	private long _idCounter = 0;
}