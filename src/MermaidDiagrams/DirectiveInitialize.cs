using System.Text.Json.Serialization;
using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public record DirectiveInitialize([property: JsonPropertyName("theme")]string Theme) : IDirective
{
	public DirectiveInitialize(ThemeVariables theme) : this(KnownThemes.Custom)
	{
		ThemeVariables = theme;
	}
	
	[JsonPropertyName("themeVariables")]
	public ThemeVariables? ThemeVariables { get; }
	
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Lines(
			"%%{",
			"  init: {",
			$"    \"theme\": \"{Theme}\"",
			"  }",
			"}%%");
	}
}