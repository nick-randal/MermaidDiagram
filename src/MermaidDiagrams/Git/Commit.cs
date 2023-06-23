using MermaidDiagrams.Contracts;
using MermaidDiagrams.Support;

namespace MermaidDiagrams.Git;

public interface ICommit : IStatement, IIdentifiable
{
	CommitType CommitType { get; }
	string? Tag { get; }
}

public record Commit(Identifier Id, CommitType CommitType, string? Tag) : ICommit
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Append($"commit");
		
		if(Id.NoId is false)
			textBuilder.Append($" id: \"{Id}\"");
		
		if (CommitType != CommitType.Normal)
		{
			textBuilder.Append($" type: {CommitType.GetShortName()}");
		}
		
		if (string.IsNullOrWhiteSpace(Tag) is false)
		{
			textBuilder.Append($" tag: \"{Tag}\"");
		}

		textBuilder.Line();
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