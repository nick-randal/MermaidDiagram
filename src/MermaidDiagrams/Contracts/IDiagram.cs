namespace MermaidDiagrams.Contracts;

public interface IDiagram : IRenderable
{
	void SetHeader(Header header);

	public T AddAnd<T>(T statement) where T : IStatement;
}