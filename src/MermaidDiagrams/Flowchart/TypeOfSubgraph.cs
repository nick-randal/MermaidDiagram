using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public record TypeOfSubgraph : IDiagramType
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
	}

	public string Name => "subgraph";
}