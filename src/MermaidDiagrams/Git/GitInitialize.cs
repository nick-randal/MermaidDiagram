using System.Text.Json.Serialization;
using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Git;

public record GitInitialize([property: JsonPropertyName("theme")]string Theme) : IDirective
{
	public GitInitialize(ThemeVariables theme) : this("default")
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

public class ThemeVariables
{
	[JsonPropertyName("git0")]
	public string Git0 { get; set; }
	
	[JsonPropertyName("git1")]
	public string Git1 { get; set; }
	
	[JsonPropertyName("git2")]
	public string Git2 { get; set; }
	
	[JsonPropertyName("git3")]
	public string Git3 { get; set; }
	
	[JsonPropertyName("git4")]
	public string Git4 { get; set; }
	
	[JsonPropertyName("git5")]
	public string Git5 { get; set; }
	
	[JsonPropertyName("git6")]
	public string Git6 { get; set; }
	
	[JsonPropertyName("git7")]
	public string Git7 { get; set; }
}