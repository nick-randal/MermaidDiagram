using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public record Link(IIdentifiable From, params (IIdentifiable Node, Edge Edge)[] To) : ILink
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		// todo node lookup if options is set and declare full Node here
		textBuilder.Append($"{From.Id}");
		foreach(var nodeEdge in To)
			textBuilder.Append($" {nodeEdge.Edge} {nodeEdge.Node.Id}");
		textBuilder.Line();
	}
}