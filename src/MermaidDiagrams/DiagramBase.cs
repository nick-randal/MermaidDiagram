using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public abstract class DiagramBase : IDiagram
{
	public virtual void SetHeader(Header header)
	{
		var existing = Renderables.FirstOrDefault(x => x is IHeader);
		if (existing is not null)
			Renderables.Remove(existing);
		
		Renderables.Insert(0, header);
	}

	public virtual void SetType(IDiagramType type)
	{
		var existing = Renderables.FirstOrDefault(x => x is IDiagramType);
		if (existing is not null)
			Renderables.Remove(existing);
		
		Renderables.Insert(0, type);
	}

	internal void AddRenderables(params IRenderable[] renderables) => Renderables.AddRange(renderables);
	
	public virtual void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		RenderFirst<IHeader>(textBuilder, renderState);
		RenderGroup<IDirective>(textBuilder, renderState);
		RenderSingle<IDiagramType>(textBuilder, renderState);
		RenderRegularStatements(textBuilder, renderState);
		RenderGroup<ClassAssign>(textBuilder, renderState);
		RenderSingle<RenderableDictionary<ClassAssign>>(textBuilder, renderState);
		RenderSingle<RenderableDictionary<ClassDef>>(textBuilder, renderState);
	}

	internal RenderableDictionary<T> GetDictionary<T>()
		where T : IRenderable
	{
		var item = Renderables.FirstOrDefault(x => x is RenderableDictionary<T>);
		if (item is RenderableDictionary<T> d)
			return d;
		
		d = new RenderableDictionary<T>();
		Renderables.Add(d);
		
		return d;
	}

	protected virtual void RenderSingle<T>(ITextBuilder textBuilder, IRenderState renderState)
		where T : IRenderable
	{
		var type = Renderables.Single(x => x is T);
		type.Render(textBuilder, renderState);
	}

	protected virtual void RenderGroup<T>(ITextBuilder textBuilder, IRenderState renderState)
		where T : IRenderable
	{
		foreach (var directive in Renderables.Where(s => s is T))
			directive.Render(textBuilder, renderState);
	}

	protected virtual void RenderFirst<T>(ITextBuilder textBuilder, IRenderState renderState)
		where T : IRenderable
	{
		var header = Renderables.FirstOrDefault(x => x is T);
		header?.Render(textBuilder, renderState);
	}

	protected virtual void RenderRegularStatements(ITextBuilder builder, IRenderState state)
	{
		using var stepper = state.StepIn();
		foreach (var statement in Renderables.Where(s => s is IStatement))
		{
			builder.Append(state.Indent);
			statement.Render(builder, state);
		}
	}

	protected readonly List<IRenderable> Renderables = new();
}