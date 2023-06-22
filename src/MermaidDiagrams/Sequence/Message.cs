using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public interface IMessage : IStatement
{
	Identifier From { get; }
	Identifier To { get; }
	string Text { get; }
}

public record Message(Identifier From, Identifier To, string Text, ArrowType Arrow) : IMessage
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"{From}{Arrow.GetShortName()}{To}:{Text}");
	}
}