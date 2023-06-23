using MermaidDiagrams.Contracts;
using MermaidDiagrams.Support;

namespace MermaidDiagrams.Git;

public class GitGraph : MermaidBase
{
	public GitGraph() : base(new DiagramType.Basic("gitGraph"))
	{
	}
	
	public IBranch MainBranch => new Branch("main");

	public virtual GitGraph SetTheme(string theme)
	{
		AddDirective(new InitializeDirective<GitInit>(new GitInit { Theme = theme}));
		return this;
	}

	public virtual GitGraph SetTheme(ThemeVariables customTheme)
	{
		AddDirective(new InitializeDirective<GitInit>(new GitInit { Theme = "custom", ThemeVariables = customTheme}));
		return this;
	}
	
	public virtual GitGraph AddDirective<T>(T directive)
		where T : IDirective
	{
		Add(directive);
		return this;
	}
	
	public ICommit CreateCommit(Identifier id, CommitType commitType = CommitType.Normal, string? tag = default)
	{
		return Add(new Commit(id, commitType, tag));
	}
	
	public IBranch CreateBranch(string name)
	{
		return Add(new Branch(name));
	}
	
	public IBranch CreateBranch(string name, Order order)
	{
		return Add(new Branch(name, order));
	}
	
	public ICheckout CreateCheckout(string name)
	{
		return Add(new Checkout(name));
	}
	
	public ICheckout CreateCheckout(IBranch branch)
	{
		return Add(new Checkout(branch));
	}
	
	public IMerge CreateMerge(string name)
	{
		return Add(new Merge(name));
	}
	
	public IMerge CreateMerge(IBranch branch)
	{
		return Add(new Merge(branch));
	}
	
	public ICherryPick CreateCherryPick(Identifier id)
	{
		return Add(new CherryPick(id));
	}
	
	public ICherryPick CreateCherryPick(ICommit commit)
	{
		return Add(new CherryPick(commit));
	}
}