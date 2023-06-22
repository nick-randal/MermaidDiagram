using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public record AutoNumber : IStatement
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line("autonumber");
	}
}