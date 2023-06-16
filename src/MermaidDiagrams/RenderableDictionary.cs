using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public class RenderableDictionary<T> : IRenderable // todo make interface and return from DiagramBase
	where T : IRenderable
{
	public IDictionary<string, T> Items { get; } = new Dictionary<string, T>(StringComparer.OrdinalIgnoreCase);

	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		foreach (var classDef in Items.Values)
			classDef.Render(textBuilder, renderState);
	}

	/*todo public T this[string key]
	{
		get => Items[key];
		set => Items[key] = value;
	}*/

	public T GetOrCreate(string key, Func<T> create)
	{
		if (Items.TryGetValue(key, out var item))
			return item;

		item = create();
		Items.Add(key, item);

		return item;
	}
}