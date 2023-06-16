using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public record ClassAssign(string Name) : IRenderable
{
	public void Add(IIdentifiable identifiable)
	{
		_items.Add(identifiable);
	}

	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"class {string.Join(",", _items.Select(x => x.Id))} {Name}");
	}

	private readonly List<IIdentifiable> _items = new();
}