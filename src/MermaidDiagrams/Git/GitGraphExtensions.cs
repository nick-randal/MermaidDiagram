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
	
	public static T Commit<T>(this T gitGraph, string tag) 
		where T : GitGraph
	{
		gitGraph.CreateCommit(Identifier.None, CommitType.Normal, tag);
		return gitGraph;
	}
	
	public static T Branch<T>(this T gitGraph, string name) 
		where T : GitGraph
	{
		gitGraph.CreateBranch(name);
		return gitGraph;
	}
	
	public static T Branch<T>(this T gitGraph, IBranch branch) 
		where T : GitGraph
	{
		gitGraph.Add(branch);
		return gitGraph;
	}
	
	public static T Checkout<T>(this T gitGraph, string name) 
		where T : GitGraph
	{
		gitGraph.CreateCheckout(name);
		return gitGraph;
	}
	
	public static T Checkout<T>(this T gitGraph, IBranch branch) 
		where T : GitGraph
	{
		gitGraph.CreateCheckout(branch);
		return gitGraph;
	}
	
	public static T Merge<T>(this T gitGraph, string name) 
		where T : GitGraph
	{
		gitGraph.CreateMerge(name);
		return gitGraph;
	}
	
	public static T Merge<T>(this T gitGraph, IBranch branch) 
		where T : GitGraph
	{
		gitGraph.CreateMerge(branch);
		return gitGraph;
	}
	
	public static T CherryPick<T>(this T gitGraph, ICommit commit) 
	
		where T : GitGraph
	{
		gitGraph.CreateCherryPick(commit);
		return gitGraph;
	}
	
	public static T CherryPick<T>(this T gitGraph, Identifier id) 
	
		where T : GitGraph
	{
		gitGraph.CreateCherryPick(id);
		return gitGraph;
	}
}