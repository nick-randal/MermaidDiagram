using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public abstract class DiagramBase : IDiagram
{
	public virtual void SetHeader(Header header)
	{
		var existing = Statements.FirstOrDefault(x => x is IHeader);
		if (existing is not null)
			Statements.Remove(existing);
		
		Statements.Insert(0, header);
	}

	public virtual void SetType(IDiagramType type)
	{
		var existing = Statements.FirstOrDefault(x => x is IDiagramType);
		if (existing is not null)
			Statements.Remove(existing);
		
		Statements.Insert(0, type);
	}

	public virtual T AddAnd<T>(T statement) where T : IStatement
	{
		Statements.Add(statement);
		return statement;
	}

	public virtual void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		var header = Statements.FirstOrDefault(x => x is IHeader);
		header?.Render(textBuilder, renderState);

		foreach (var directive in Statements.Where(s => s is IDirective))
			directive.Render(textBuilder, renderState);
		
		var type = Statements.Single(x => x is IDiagramType);
		type.Render(textBuilder, renderState);
	}
	
	internal readonly List<IStatement> Statements = new();
}