using MermaidDiagrams.Support;

namespace MermaidDiagrams.Git;

public class GitGraph : MermaidBase
{
	public GitGraph() : base(new DiagramType.Basic("gitGraph"))
	{
	}
	
	public GitGraph CreateCommit(string message = "")
	{
		return this;
	}
}