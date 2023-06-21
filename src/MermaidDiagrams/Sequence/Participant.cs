using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public interface IParticipant : IStatement
{
	string Name { get; }
	
	string? Alias { get; }
	
	bool UseActor { get; }
}

public record Participant(string Name, string? Alias, bool UseActor = false) : IParticipant
{
	public Participant(string name, bool useActor = false) : this(name, null, useActor)
	{
	}

	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Append(UseActor ? "actor " : "participant ");
		textBuilder.Append(Name);
		
		if(string.IsNullOrWhiteSpace(Alias))
			textBuilder.Line();
		else
			textBuilder.Line($" as {Alias}");
	}
}