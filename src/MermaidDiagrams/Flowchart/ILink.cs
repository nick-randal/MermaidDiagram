using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public interface ILink : IStatement
{
	public IIdentifiable From { get; }

	public (IIdentifiable Node, Edge Edge)[] To { get; }
}