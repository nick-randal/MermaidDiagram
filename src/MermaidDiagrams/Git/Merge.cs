using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Git;

public interface IMerge : IStatement
{
	string Name { get; }
}

public record Merge(string Name) : IMerge
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"merge \"{Name}\"");
	}
}