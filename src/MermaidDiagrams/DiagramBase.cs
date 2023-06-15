using System.Text;
using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public abstract class DiagramBase<TDiagram, TOptions> : IDiagram<TDiagram, TOptions>
	where TOptions : IRenderOptions
{
	public virtual void SetHeader(Header header)
	{
		var existing = Statements.FirstOrDefault(x => x is IHeader);
		if (existing is not null)
			Statements.Remove(existing);
		
		Statements.Insert(0, header);
	}

	public void SetType(IDiagramType type)
	{
		var existing = Statements.FirstOrDefault(x => x is IDiagramType);
		if (existing is not null)
			Statements.Remove(existing);
		
		Statements.Insert(0, type);
	}

	public T AddAnd<T>(T statement) where T : IStatement
	{
		Statements.Add(statement);
		return statement;
	}

	public abstract TDiagram Add(params IStatement[] statements);

	public abstract string Render(TOptions options);

	public virtual void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		var header = Statements.FirstOrDefault(x => x is IHeader);
		header?.Render(textBuilder, renderState);

		foreach (var directive in Statements.Where(s => s is IDirective))
			directive.Render(textBuilder, renderState);
		
		var type = Statements.Single(x => x is IDiagramType);
		type.Render(textBuilder, renderState);
	}
	
	protected readonly List<IStatement> Statements = new();
}