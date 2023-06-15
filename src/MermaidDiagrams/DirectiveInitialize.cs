using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public record DirectiveInitialize(string Theme) : IDirective
{
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