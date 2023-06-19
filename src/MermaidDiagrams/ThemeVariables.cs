using System.Text.Json.Serialization;

namespace MermaidDiagrams;

public class ThemeVariables
{
	[JsonPropertyName("primaryColor")]
	public string PrimaryColor { get; set; }

	[JsonPropertyName("primaryTextColor")]
	public string PrimaryTextColor { get; set; }

	[JsonPropertyName("primaryBorderColor")]
	public string PrimaryBorderColor { get; set; }

	[JsonPropertyName("lineColor")]
	public string LineColor { get; set; }

	[JsonPropertyName("secondaryColor")]
	public string SecondaryColor { get; set; }

	[JsonPropertyName("tertiaryColor")]
	public string TertiaryColor { get; set; }
}