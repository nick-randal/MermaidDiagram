using System.Text;
using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public record Literal(string Content) : ILiteral
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line(Content);
	}
}