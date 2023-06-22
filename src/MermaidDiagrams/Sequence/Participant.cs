using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public interface IParticipant : IStatement, IIdentifiable
{
	Text Alias { get; }
	
	bool UseActor { get; }
}

public record Participant(Identifier Id, Text Alias, bool UseActor = false) : IParticipant
{
	public Participant(Identifier id, bool useActor = false) : this(id, Text.Empty, useActor)
	{
	}

	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Append(UseActor ? "actor " : "participant ");
		textBuilder.Append(Id.ToString());
		
		if(Alias.IsEmpty)
			textBuilder.Line();
		else
			textBuilder.Line($" as {Alias}");
	}
}