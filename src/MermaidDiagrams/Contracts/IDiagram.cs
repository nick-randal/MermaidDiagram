namespace MermaidDiagrams.Contracts;

public interface IDiagram<TDiagram, in TOptions> : IRenderable
	where TOptions : IRenderOptions
{
	void SetHeader(Header header);

	string Render(TOptions options);

	TDiagram Add(params IStatement[] statements);
	
	public T AddAnd<T>(T statement) where T : IStatement;
}