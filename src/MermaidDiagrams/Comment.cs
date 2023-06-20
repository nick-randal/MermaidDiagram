using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public interface IComment : IStatement
{
}

public record Comment(string Content) : IComment
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"%% {Content}");
	}
}