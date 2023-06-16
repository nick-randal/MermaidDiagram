using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public interface ILink : IStatement
{
	public INode From { get; }

	public (INode Node, Edge Edge)[] To { get; }
}