using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Support;

public abstract class MermaidBase : IRenderable
{
	protected MermaidBase(IDiagramType type)
	{
		_renderables = new(OnItemChange);
			
		var existing = _renderables.FirstOrDefaultOf<IDiagramType>();
		if (existing is not null)
			_renderables.Remove(existing);

		_renderables.Insert(0, type);
	}

	internal virtual void OnItemChange(IRenderable item, ItemChange change)
	{
	}

	public bool TryAdd<T>(T renderable)
		where T : IRenderable
	{
		if (_renderables.FirstOrDefaultOf<T>() is null)
		{
			Add(renderable);
			return true;
		}
		
		return false;
	}
	
	public T Add<T>(T renderable)
		where T : IRenderable
	{
		_renderables.Add(renderable);
		return renderable;
	}

	public void AddRange(params IRenderable[] renderables) => _renderables.AddRange(renderables);

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
		var type = _renderables.SingleOf<T>();
		type.Render(textBuilder, renderState);
	}

	protected virtual void RenderGroup<T>(ITextBuilder textBuilder, IRenderState renderState)
		where T : IRenderable
	{
		foreach (var directive in _renderables.OfType<T>())
			directive.Render(textBuilder, renderState);
	}

	protected virtual void RenderFirst<T>(ITextBuilder textBuilder, IRenderState renderState)
		where T : IRenderable
	{
		var header = _renderables.FirstOrDefaultOf<T>();
		header?.Render(textBuilder, renderState);
	}

	protected virtual void RenderRegularStatements(ITextBuilder builder, IRenderState state)
	{
		using var stepper = state.StepIn();
		foreach (var statement in _renderables.OfType<IStatement>())
		{
			builder.Append(state.Indent);
			statement.Render(builder, state);
		}
	}

	protected T GetOrCreate<T>()
		where T : IRenderable, new()
	{
		var defs = _renderables.FirstOrDefaultOf<T>();
		if (defs is { } cd)
			return cd;

		cd = new T();
		Add(cd);

		return cd;
	}

	private readonly ManagedList<IRenderable> _renderables;
}