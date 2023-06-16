using System.Collections;
using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public record ClassDef(string Name, Dictionary<string, string> Styles) : IRenderable
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"classDef {Name} {string.Join(",", Styles.Select(x => $"{x.Key.ToLowerInvariant()}:{x.Value}"))}");
	}

	public static readonly ClassDef Invisible = new(InvisibleName, new Dictionary<string, string>
	{
		{ "stroke", Transparent },
		{ "fill", Transparent },
		{ "color", Transparent },
		{ "stroke-width", ":0px	" }
	});

	public const string 
		InvisibleName = "invis",
		Transparent = "#00000000";
}