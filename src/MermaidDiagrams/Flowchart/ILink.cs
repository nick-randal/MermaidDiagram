using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public interface ILink : IStatement
{
	public Identifier From { get; }

	public (Identifier To, Edge Edge)[] To { get; }
}