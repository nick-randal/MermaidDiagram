using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

internal record TypeOfSequence : IDiagramType
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line(Name);
	}
	
	public string Name => "sequenceDiagram";
}