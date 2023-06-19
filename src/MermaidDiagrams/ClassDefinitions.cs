using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public class ClassDefinitions : IRenderable
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		foreach (var pair in _items)
		{
			textBuilder.Line($"class {string.Join(",", pair.Value.Assigns)} {pair.Key}");
			
		}
		
		foreach (var pair in _items)
		{
			textBuilder.Line($"classDef {pair.Key} {string.Join(",", pair.Value.Styles.Select(x => $"{x.Key.ToLowerInvariant()}:{x.Value}"))}");
		}
	}

	public ClassDef GetOrCreate(string key, Func<string, ClassDef> create)
	{
		if (_items.TryGetValue(key, out var item))
			return item;

		item = create(key);
		_items.Add(key, item);

		return item;
	}

	private readonly Dictionary<string, ClassDef> _items  = new(StringComparer.OrdinalIgnoreCase);
}

