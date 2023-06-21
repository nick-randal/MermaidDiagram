using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

internal record BasicDiagramType(string Name) : IDiagramType
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line(Name);
	}
}