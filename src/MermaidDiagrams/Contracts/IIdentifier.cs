using MermaidDiagrams.Flowchart;

namespace MermaidDiagrams.Contracts;

public interface IIdentifiable
{
	Identifier Id { get; }
}