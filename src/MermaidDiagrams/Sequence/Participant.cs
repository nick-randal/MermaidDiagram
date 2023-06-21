using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public interface IParticipant : IStatement, IIdentifiable
{
	string? Alias { get; }
	
	bool UseActor { get; }
}

public record Participant(Identifier Id, string? Alias, bool UseActor = false) : IParticipant
{
	public Participant(Identifier id, bool useActor = false) : this(id, null, useActor)
	{
	}

	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Append(UseActor ? "actor " : "participant ");
		textBuilder.Append(Id.ToString());
		
		if(string.IsNullOrWhiteSpace(Alias))
			textBuilder.Line();
		else
			textBuilder.Line($" as {Alias}");
	}
}