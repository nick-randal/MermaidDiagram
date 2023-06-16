using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

using This = ISubgraph;

public interface ISubgraph : IStatement, IIdentifiable
{
	Text Label { get; }

	This SetDirection(FlowDirection direction);
}

public class FlowchartSubgraph : FlowchartBase, ISubgraph
{
	public FlowchartSubgraph(Text label, Identifier? id = null)
	{
		Label = label;
		Id = id ?? new Identifier(string.Empty);
	}

	public Text Label { get; }
	
	public Identifier Id { get; }
	
	public This SetDirection(FlowDirection direction)
	{
		Statements.Add(new Literal($"direction {direction.GetShortName()}"));
		return this;
	}

	public override void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line(Id.NoId ? $"subgraph {Label}" : $"subgraph {Id} [{Label}]");

		RenderRegularStatements(textBuilder, renderState);
		
		textBuilder.Line($"{renderState.Indent}end");
	}

	public override void SetHeader(Header header) => throw new NotSupportedException();

	public override void SetType(IDiagramType type) => throw new NotSupportedException();
}