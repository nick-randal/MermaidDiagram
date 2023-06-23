using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Support;

public abstract class MermaidBase : IRenderable
{
	protected MermaidBase(IDiagramType type)
	{
		var existing = _renderables.FirstOrDefault(x => x is IDiagramType);
		if (existing is not null)
			_renderables.Remove(existing);

		Renderables.Insert(0, type);
	}

	public bool TryAdd<T>(T renderable)
		where T : IRenderable
	{
		if (!Renderables.Any(r => r is T))
		{
			Add(renderable);
			return true;
		}
		
		return false;
	}
	
	public T Add<T>(T renderable)
		where T : IRenderable
	{
		Renderables.Add(renderable);
		return renderable;
	}

	public void AddRange(params IRenderable[] renderables) => Renderables.AddRange(renderables);

	public virtual string Render()
	{
		var state = new RenderState();
		var builder = new TextBuilder();

		Render(builder, state);

		return builder.Text;
	}

	public virtual void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		RenderFirst<IHeader>(textBuilder, renderState);
		RenderGroup<IDirective>(textBuilder, renderState);
		RenderSingle<IDiagramType>(textBuilder, renderState);
		RenderRegularStatements(textBuilder, renderState);
		using var stepper = renderState.StepIn();
		RenderFirst<ClassDefinitions>(textBuilder, renderState);
	}

	protected IIdentifiable this[Identifier id] => (IIdentifiable)_renderables.Single(s => s is IIdentifiable i && i.Id.Equals(id));

	protected T GetRenderableOrThrow<T>(Identifier id)
		where T : class, IIdentifiable
		=> this[id] as T ?? throw new KeyNotFoundException($"{nameof(T)} not found for id: {id}");

	protected virtual void RenderSingle<T>(ITextBuilder textBuilder, IRenderState renderState)
		where T : IRenderable
	{
		var type = _renderables.Single(x => x is T);
		type.Render(textBuilder, renderState);
	}

	protected virtual void RenderGroup<T>(ITextBuilder textBuilder, IRenderState renderState)
		where T : IRenderable
	{
		foreach (var directive in _renderables.Where(s => s is T))
			directive.Render(textBuilder, renderState);
	}

	protected virtual void RenderFirst<T>(ITextBuilder textBuilder, IRenderState renderState)
		where T : IRenderable
	{
		var header = _renderables.FirstOrDefault(x => x is T);
		header?.Render(textBuilder, renderState);
	}

	protected virtual void RenderRegularStatements(ITextBuilder builder, IRenderState state)
	{
		using var stepper = state.StepIn();
		foreach (var statement in _renderables.Where(s => s is IStatement))
		{
			builder.Append(state.Indent);
			statement.Render(builder, state);
		}
	}

	protected T GetOrCreate<T>()
		where T : IRenderable, new()
	{
		var defs = _renderables.FirstOrDefault(x => x is T);
		if (defs is T cd)
			return cd;

		cd = new T();
		Add(cd);

		return cd;
	}

	private readonly List<IRenderable> _renderables = new();
}