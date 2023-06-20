using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public record NodeId(Identifier Id) : IIdentifiable
{
	public static implicit operator NodeId(string id) => new((Identifier)id);
}