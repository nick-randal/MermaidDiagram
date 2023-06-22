namespace MermaidDiagrams.Git;

public static class GitGraphExtensions
{
	public static T Commit<T>(this T gitGraph, string message = "") 
		where T : GitGraph
	{
		gitGraph.CreateCommit(message);

		return gitGraph;
	}
}