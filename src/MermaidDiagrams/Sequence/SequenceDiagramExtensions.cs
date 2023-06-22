using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public static class SequenceDiagramExtensions
{
	public static T Activate<T>(this T sequence, IIdentifiable item)
		where T : SequenceBase => sequence.AddAnd(new Activation(item));
	
	public static T Deactivate<T>(this T sequence, IIdentifiable item)
		where T : SequenceBase => sequence.AddAnd(new Activation(item, false));
	
	public static T Activate<T>(this T sequence, Identifier id)
		where T : SequenceBase => sequence.AddAnd(new Activation(id));
	
	public static T Deactivate<T>(this T sequence, Identifier id)
		where T : SequenceBase => sequence.AddAnd(new Activation(id, false));
	
	public static T Participant<T>(this T sequence, Identifier id, bool useActor = false)
		where T : SequenceBase
	{
		sequence.CreateParticipant(id, useActor);
		return sequence;
	}
	
	public static T Participant<T>(this T sequence, Identifier id, string alias, bool useActor = false)
		where T : SequenceBase
	{
		sequence.CreateParticipant(id, alias, useActor);
		return sequence;
	}
	
	public static T Message<T>(this T sequence, Identifier a, Identifier b, string text, ArrowType arrow = ArrowType.SolidLineArrow, bool? activate = null)
		where T : SequenceBase
	{
		sequence.CreateMessage(a, b, text, arrow, activate);
		return sequence;
	}
	
	public static T Note<T>(this T sequence, Identifier id, string text, NotePosition position = NotePosition.Over, Identifier? idTo = null)
		where T : SequenceBase
	{
		sequence.CreateNote (id, text, position, idTo);
		return sequence;
	}

	public static T Loop<T>(this T sequence, string? label, Action<Loop> builder)
		where T : SequenceBase
	{
		var loop = sequence.CreateLoop(label);
		builder(loop);
		return sequence;
	}

	public static T SetAutoNumbering<T>(this T sequence)
		where T : SequenceBase
	{
		sequence.TryAdd(new AutoNumber());
		return sequence;
	}
}