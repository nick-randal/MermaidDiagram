using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public record ClassAssign(string Name, params string[] Nodes) : ISpecialStatement
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"class {string.Join(",", Nodes)} {Name}");
	}
}