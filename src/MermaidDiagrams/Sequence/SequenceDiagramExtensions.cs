using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public static class SequenceDiagramExtensions
{
	public static T Activate<T>(this T sequence, IIdentifiable item)
		where T : SequenceDiagram => sequence.AddAnd(new Activation(item));
	
	public static T Deactivate<T>(this T sequence, IIdentifiable item)
		where T : SequenceDiagram => sequence.AddAnd(new Activation(item, false));
	
	public static T Activate<T>(this T sequence, Identifier id)
		where T : SequenceDiagram => sequence.AddAnd(new Activation(id));
	
	public static T Deactivate<T>(this T sequence, Identifier id)
		where T : SequenceDiagram => sequence.AddAnd(new Activation(id, false));
	
	public static T Participant<T>(this T sequence, Identifier id, bool useActor = false)
		where T : SequenceDiagram
	{
		sequence.CreateParticipant(id, useActor);
		return sequence;
	}
	
	public static T Participant<T>(this T sequence, Identifier id, string alias, bool useActor = false)
		where T : SequenceDiagram
	{
		sequence.CreateParticipant(id, alias, useActor);
		return sequence;
	}
	
	public static T Message<T>(this T sequence, Identifier a, Identifier b, string text, ArrowType arrow = ArrowType.SolidLineArrow)
		where T : SequenceDiagram
	{
		sequence.CreateMessage(a, b, text, arrow);
		return sequence;
	}
	
	public static T SetAutoNumbering<T>(this T sequence)
		where T : SequenceDiagram
	{
		sequence.TryAdd(new AutoNumber());
		return sequence;
	}
}