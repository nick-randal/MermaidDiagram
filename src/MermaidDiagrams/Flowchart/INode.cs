using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public interface INode : IStatement, IIdentifiable
{
	Shape Shape { get; }
	
	Text Text { get; }
}