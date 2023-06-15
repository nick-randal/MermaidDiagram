using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public record StandardOptions : IRenderOptions
{
	public string Indent { get; init; } = "  ";
}