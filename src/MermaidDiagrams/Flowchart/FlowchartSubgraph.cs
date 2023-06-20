using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public class FlowchartSubgraph : FlowchartBase, IStatement
{
	public FlowchartSubgraph(Text label, Identifier? id = null)
	{
		Label = label;
		Id = id ?? new Identifier(string.Empty);
	}

	public Text Label { get; }
	
	public Identifier Id { get; }
	
	public FlowchartSubgraph SetDirection(FlowDirection direction)
	{
		Renderables.Add(new Literal($"direction {direction.GetShortName()}"));
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