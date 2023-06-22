using MermaidDiagrams.Contracts;
using MermaidDiagrams.Support;

namespace MermaidDiagrams.Sequence;

public enum NotePosition
{
	[Display(ShortName = "left of")]
	LeftOf,
	
	[Display(ShortName = "right of")]
	RightOf,
	
	[Display(ShortName = "over")]
	Over
}

public record Note(Identifier Id, Text Text, NotePosition Position, Identifier? IdTo = default) : IStatement
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"Note {Position.GetShortName()} {Id}{(IdTo is null ? string.Empty : $",{IdTo}")}: {Text}");
	}
}