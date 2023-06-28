using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public record ClassDef
{
	public ClassDef(string name)
	{
		Name = name;
		_styles = new(StringComparer.OrdinalIgnoreCase);
		_assigns = new();
	}

	public string Name { get; }

	public ClassDef Style(string key, string value)
	{
		_styles[key] = value;
		return this;
	}
	
	public ClassDef Assign(params IIdentifiable[] items)
	{
		_assigns.AddRange(items.Select(x => x.Id));
		return this;
	}

	
	public ClassDef Assign(params Identifier[] ids)
	{
		_assigns.AddRange(ids);
		return this;
	}
	
	public IReadOnlyDictionary<string, string> Styles => _styles;
	
	public IReadOnlyList<Identifier> Assigns => _assigns;

	private readonly Dictionary<string, string> _styles;
	
	private readonly List<Identifier> _assigns;

	public static readonly ClassDef Invisible = new ClassDef(InvisibleName)
		.Style("stroke", Transparent).Style("fill", Transparent).Style("color", Transparent).Style("stroke-width", ":0px");

	public const string
		InvisibleName = "invis",
		Transparent = "#00000000";
}