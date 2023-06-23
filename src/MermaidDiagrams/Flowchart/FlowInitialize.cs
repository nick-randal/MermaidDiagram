using System.Text.Json;
using System.Text.Json.Serialization;
using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public record FlowInitialize(string Theme) : IDirective
{
	public FlowInitialize(ThemeVariables theme) : this("base")
	{
		ThemeVariables = theme;
	}
	
	[JsonPropertyName("themeVariables")]
	public ThemeVariables? ThemeVariables { get; }
	
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		var init = new FlowInit
		{
			Theme = Theme,
			ThemeVariables = ThemeVariables
		};
		
		textBuilder.Lines($@"%%{{init: {JsonSerializer.Serialize(init, _jsonSerializerOptions)}}}%%");
	}

	private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		WriteIndented = true,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
	};
	
	private class FlowInit
	{
		[property: JsonPropertyName("theme")]
		public string Theme { get; init; }
		
		[JsonPropertyName("themeVariables")]
		public ThemeVariables? ThemeVariables { get; init; }
	}
}