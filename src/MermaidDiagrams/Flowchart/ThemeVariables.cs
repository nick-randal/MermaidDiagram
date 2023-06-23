using System.Text.Json.Serialization;

namespace MermaidDiagrams.Flowchart;

public class ThemeVariables
{
	[JsonPropertyName("primaryColor")]
	public Rgb PrimaryColor { get; set; }

	[JsonPropertyName("primaryTextColor")]
	public Rgb PrimaryTextColor { get; set; }

	[JsonPropertyName("primaryBorderColor")]
	public Rgb PrimaryBorderColor { get; set; }

	[JsonPropertyName("lineColor")]
	public Rgb LineColor { get; set; }

	[JsonPropertyName("secondaryColor")]
	public Rgb SecondaryColor { get; set; }

	[JsonPropertyName("tertiaryColor")]
	public Rgb TertiaryColor { get; set; }
}