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
	
	public void Assign(IIdentifiable item)
	{
		_assigns.Add(item);
	}
	
	public IReadOnlyDictionary<string, string> Styles => _styles;
	
	public IReadOnlyList<IIdentifiable> Assigns => _assigns;

	private readonly Dictionary<string, string> _styles;
	
	private readonly List<IIdentifiable> _assigns;

	public static readonly ClassDef Invisible = new ClassDef(InvisibleName)
		.Style("stroke", Transparent).Style("fill", Transparent).Style("color", Transparent).Style("stroke-width", ":0px");

	public const string
		InvisibleName = "invis",
		Transparent = "#00000000";
}