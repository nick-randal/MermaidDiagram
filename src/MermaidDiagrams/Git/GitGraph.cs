using MermaidDiagrams.Support;

namespace MermaidDiagrams.Git;

public class GitGraph : MermaidBase
{
	public GitGraph() : base(new DiagramType.Basic("gitGraph"))
	{
	}
	
	public ICommit CreateCommit(Identifier id, CommitType commitType = CommitType.Normal, string? tag = default)
	{
		return Add(new Commit(id, commitType, tag));
	}
}