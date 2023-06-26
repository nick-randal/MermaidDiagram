using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public record Link(Identifier From, params (Identifier To, Edge Edge)[] To) : ILink
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Append($"{From}");
		foreach(var nodeEdge in To)
			textBuilder.Append($" {nodeEdge.Edge} {nodeEdge.To}");
		textBuilder.Line();
	}
}