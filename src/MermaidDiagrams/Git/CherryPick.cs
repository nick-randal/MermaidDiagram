using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Git;

public interface ICherryPick : IStatement
{
}

public record CherryPick(Identifier Id) : ICherryPick
{
	public CherryPick(ICommit commit) : this(commit.Id)
	{
	}
	
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"cherry-pick id: \"{Id}\"");
	}
}