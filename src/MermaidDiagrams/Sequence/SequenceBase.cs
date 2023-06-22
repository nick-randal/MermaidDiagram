using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public abstract class SequenceBase : MermaidBase, IStatement
{
	protected SequenceBase(IDiagramType diagramType) : base(diagramType)
	{
	}
	
	public new IParticipant this[Identifier id] => GetRenderableOrThrow<IParticipant>(id);
	
	public Participant CreateParticipant(Identifier id, bool useActor = false) => Add(new Participant(id, useActor));
	
	public Participant CreateParticipant(Identifier id, Text alias, bool useActor = false) => Add(new Participant(id, alias, useActor));

	public Message CreateMessage(
		Identifier a, Identifier b, Text text, ArrowType arrow = ArrowType.SolidLineArrow, bool? activate = null
	) 
		=> Add(new Message(a, b, text, arrow, activate));
	
	public Note CreateNote(Identifier id, Text text, NotePosition position = NotePosition.Over, Identifier? idTo = null) 
		=> Add(new Note (id, text, position, idTo));

	public Loop CreateLoop(Text label) => Add(new Loop(label));
	
	public Break CreateBreak(Text label) => Add(new Break(label));
	
	public Alternate CreateAlternate() => Add(new Alternate());
	
	public Alternate CreateAlternate(Text ifLabel, Text elseLabel) => Add(new Alternate(ifLabel, elseLabel));
	
	public Optional CreateOptional(Text label) => Add(new Optional(label));
	
	public Parallel CreateParallel() => Add(new Parallel());
	
	public Critical CreateCritical() => Add(new Critical());
	
	public Highlight CreateHighlight(Rgb backgroundColor) => Add(new Highlight(backgroundColor));
}