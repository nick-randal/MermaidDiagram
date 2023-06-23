using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Git;

public interface IBranch : IStatement
{
	string Name { get; }
}

public record Branch(string Name) : IBranch
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"branch \"{Name}\"");
	}
}