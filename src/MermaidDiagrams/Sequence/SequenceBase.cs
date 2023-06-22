using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public abstract class SequenceBase : MermaidBase
{
	protected SequenceBase(IDiagramType diagramType) : base(diagramType)
	{
	}
	
	public new IParticipant this[Identifier id] => GetRenderableOrThrow<IParticipant>(id);
	
	public Participant CreateParticipant(Identifier id, bool useActor = false) => Add(new Participant(id, useActor));
	
	public Participant CreateParticipant(Identifier id, string alias, bool useActor = false) => Add(new Participant(id, alias, useActor));

	public Message CreateMessage(
		Identifier a, Identifier b, string text, ArrowType arrow = ArrowType.SolidLineArrow, bool? activate = null
	) 
		=> Add(new Message(a, b, text, arrow, activate));
	
	public Note CreateNote(Identifier id, string text, NotePosition position = NotePosition.Over, Identifier? idTo = null) 
		=> Add(new Note (id, text, position, idTo));

	public Loop CreateLoop(string? label) => Add(new Loop(label));
}