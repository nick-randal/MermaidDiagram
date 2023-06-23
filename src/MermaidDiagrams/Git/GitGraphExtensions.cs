namespace MermaidDiagrams.Git;

public static class GitGraphExtensions
{
	public static T Commit<T>(this T gitGraph, CommitType commitType = CommitType.Normal, string? tag = default) 
		where T : GitGraph
	{
		gitGraph.CreateCommit(Identifier.None, commitType, tag);
		return gitGraph;
	}
	
	public static T Commit<T>(this T gitGraph, Identifier id, CommitType commitType = CommitType.Normal, string? tag = default) 
		where T : GitGraph
	{
		gitGraph.CreateCommit(id, commitType, tag);
		return gitGraph;
	}
}