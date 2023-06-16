namespace MermaidDiagrams.Contracts;

public interface IDiagram : IRenderable
{
	void SetHeader(Header header);

	void SetType(IDiagramType type);
}