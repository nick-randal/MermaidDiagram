using MermaidDiagrams.Contracts;
using MermaidDiagrams.Support;

namespace MermaidDiagrams.Flowchart;

public class FlowchartSubgraph : FlowchartBase, IIdentifiable
{
	internal FlowchartSubgraph(FlowchartBase parent, Text label, Identifier? id = null) : base(parent, new DiagramType.None())
	{
		Label = label;
		Id = id ?? new Identifier(string.Empty);
	}

	public Text Label { get; }
	
	public Identifier Id { get; }
	
	public FlowchartSubgraph SetDirection(FlowDirection direction)
	{
		this.Literal($"direction {direction.GetShortName()}");
		return this;
	}

	public override void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line(Id.NoId ? $"subgraph {Label}" : $"subgraph {Id} [{Label}]");
		RenderRegularStatements(textBuilder, renderState);
		textBuilder.Line($"{renderState.Indent}end");
	}
}