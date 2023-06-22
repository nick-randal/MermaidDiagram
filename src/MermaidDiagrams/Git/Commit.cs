using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Git;

public interface ICommit : IStatement, IIdentifiable
{
	string Type { get; }
	string Tag { get; }
}

public record Commit(Identifier Id, string Type, string Tag) : ICommit
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"commit {Id} {Type} {Tag}");	
	}
}

public enum CommitType
{
	[Display(ShortName = "")]
	Normal,
	
	[Display(ShortName = "REVERSE")]
	Reverse,
	
	[Display(ShortName = "HIGHLIGHT")]
	Highlight
}