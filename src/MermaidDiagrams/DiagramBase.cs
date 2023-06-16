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

	public virtual void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		RenderFirst<IHeader>(textBuilder, renderState);
		RenderGroup<IDirective>(textBuilder, renderState);
		RenderSingle<IDiagramType>(textBuilder, renderState);
		RenderRegularStatements(textBuilder, renderState);
		RenderGroup<ClassAssign>(textBuilder, renderState);
		RenderGroup<ClassDef>(textBuilder, renderState);
	}

	protected virtual void RenderSingle<T>(ITextBuilder textBuilder, IRenderState renderState)
	{
		var type = Statements.Single(x => x is T);
		type.Render(textBuilder, renderState);
	}

	protected virtual void RenderGroup<T>(ITextBuilder textBuilder, IRenderState renderState)
	{
		foreach (var directive in Statements.Where(s => s is T))
			directive.Render(textBuilder, renderState);
	}

	protected virtual void RenderFirst<T>(ITextBuilder textBuilder, IRenderState renderState)
	{
		var header = Statements.FirstOrDefault(x => x is T);
		header?.Render(textBuilder, renderState);
	}

	protected virtual void RenderRegularStatements(ITextBuilder builder, IRenderState state)
	{
		using var stepper = state.StepIn();
		foreach (var statement in Statements.Where(s => s is not ISpecialStatement))
		{
			if (statement is not IComment)
				builder.Append(state.Indent);
			statement.Render(builder, state);
		}
	}

	internal readonly List<IStatement> Statements = new();
}