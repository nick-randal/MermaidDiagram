using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public record TypeOfNone : IDiagramType
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
	}

	public string Name => string.Empty;
}