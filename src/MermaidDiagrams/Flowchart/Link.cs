using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public record Link(INode From, INode To, Edge Edge, Text Label) : ILink
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"{From.Id} {Edge} {(Label.IsEmpty ? string.Empty : $"|{Label}|")}{To.Id}");
	}
}