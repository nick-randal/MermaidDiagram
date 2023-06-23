using System.Text.Json.Serialization;

namespace MermaidDiagrams.Flowchart;

public sealed record FlowchartInit
{
	[property: JsonPropertyName("theme")]
	public string Theme { get; init; }

	[JsonPropertyName("themeVariables")]
	public ThemeVariables? ThemeVariables { get; init; }
}