using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public abstract class FlowchartBase : DiagramBase
{
	public INode Node() => AddAnd(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, Shape.Box));

	public INode Node(string id) => AddAnd(Flowchart.Node.Create(id, string.Empty, Shape.Box));

	public INode Node(Shape style) => AddAnd(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, style));

	public INode Node(Text text, Shape style) => AddAnd(Flowchart.Node.Create(Flowchart.Node.NextId(), text, style));

	public INode Node(string id, Text text, Shape style) => AddAnd(Flowchart.Node.Create(id, text, style));

	public INode this[string id]
		=> Statements.Single(s => s is INode node && node.Id.Equals(id, StringComparison.OrdinalIgnoreCase)) as INode
			?? throw new KeyNotFoundException($"Node not found for id: {id}");

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
}