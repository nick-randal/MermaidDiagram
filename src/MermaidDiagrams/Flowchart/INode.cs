using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public interface INode : IStatement
{
	string Id { get; }
	
	Shape Shape { get; }
	
	Text Text { get; }
}