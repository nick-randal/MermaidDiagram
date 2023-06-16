namespace MermaidDiagrams.Contracts;

public interface IDiagramType : IRenderable
{
	string Name { get; }
}