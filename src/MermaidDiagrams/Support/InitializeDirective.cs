using System.Text.Json;
using System.Text.Json.Serialization;
using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Support;

public record InitializeDirective<T>(T Init) : IDirective
	where T: class
{
	public virtual void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Lines($@"%%{{init: {JsonSerializer.Serialize(Init, JsonSerializerOptions)}}}%%");
	}

	protected static readonly JsonSerializerOptions JsonSerializerOptions = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		WriteIndented = true,
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
	};
}