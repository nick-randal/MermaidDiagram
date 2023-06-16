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
		RenderHeader(textBuilder, renderState);
		RenderDirectives(textBuilder, renderState);
		RenderDiagramType(textBuilder, renderState);
		RenderStatements(textBuilder, renderState);
		RenderClassAssignments(textBuilder, renderState);
		RenderClassDefinitions(textBuilder, renderState);
	}

	protected virtual void RenderDiagramType(ITextBuilder textBuilder, IRenderState renderState)
	{
		var type = Statements.Single(x => x is IDiagramType);
		type.Render(textBuilder, renderState);
	}

	protected virtual void RenderDirectives(ITextBuilder textBuilder, IRenderState renderState)
	{
		foreach (var directive in Statements.Where(s => s is IDirective))
			directive.Render(textBuilder, renderState);
	}

	protected virtual void RenderHeader(ITextBuilder textBuilder, IRenderState renderState)
	{
		var header = Statements.FirstOrDefault(x => x is IHeader);
		header?.Render(textBuilder, renderState);
	}

	protected virtual void RenderStatements(ITextBuilder builder, IRenderState state)
	{
		using var stepper = state.StepIn();
		foreach (var statement in Statements.Where(s => s is not ISpecialStatement))
		{
			if (statement is not IComment)
				builder.Append(state.Indent);
			statement.Render(builder, state);
		}
	}
	
	protected virtual void RenderClassAssignments(ITextBuilder textBuilder, IRenderState renderState)
	{
		foreach (var classAssignment in Statements.Where(s => s is ClassAssign))
			classAssignment.Render(textBuilder, renderState);
	}

	protected virtual void RenderClassDefinitions(ITextBuilder textBuilder, IRenderState renderState)
	{
		foreach (var classDef in Statements.Where(s => s is ClassDef))
			classDef.Render(textBuilder, renderState);
	}

	internal readonly List<IStatement> Statements = new();
}