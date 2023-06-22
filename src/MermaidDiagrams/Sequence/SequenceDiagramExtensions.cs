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
	
	public static T Participant<T>(this T sequence, Identifier id, Text alias, bool useActor = false)
		where T : SequenceBase
	{
		sequence.CreateParticipant(id, alias, useActor);
		return sequence;
	}
	
	public static T Message<T>(this T sequence, IIdentifiable a, IIdentifiable b, Text text, ArrowType arrow = ArrowType.SolidLineArrow, bool? activate = null)
		where T : SequenceBase
	{
		sequence.CreateMessage(a.Id, b.Id, text, arrow, activate);
		return sequence;
	}
	
	public static T Message<T>(this T sequence, Identifier a, Identifier b, Text text, ArrowType arrow = ArrowType.SolidLineArrow, bool? activate = null)
		where T : SequenceBase
	{
		sequence.CreateMessage(a, b, text, arrow, activate);
		return sequence;
	}
	
	public static T Note<T>(this T sequence, Identifier id, Text text, NotePosition position = NotePosition.Over, Identifier? idTo = null)
		where T : SequenceBase
	{
		sequence.CreateNote (id, text, position, idTo);
		return sequence;
	}

	public static T Loop<T>(this T sequence, Text label, Action<Loop> builder)
		where T : SequenceBase
	{
		var loop = sequence.CreateLoop(label);
		builder(loop);
		return sequence;
	}
	
	public static T Alternate<T>(this T sequence, Action<SubSequenceBlock> ifBuilder, Action<SubSequenceBlock> elseBuilder)
		where T : SequenceBase
	{
		var alt = sequence.CreateAlternate();
		ifBuilder(alt.IfBlock);
		elseBuilder(alt.ElseBlock);
		return sequence;
	}
	
	public static T Optional<T>(this T sequence, Text label, Action<Optional> builder)
		where T : SequenceBase
	{
		var opt = sequence.CreateOptional(label);
		builder(opt);
		return sequence;
	}
	
	public static T Parallel<T>(this T sequence, Action<SubSequenceBlock> firstBuilder, params Action<SubSequenceBlock>[] andBuilders)
		where T : SequenceBase
	{
		var par = sequence.CreateParallel();
		firstBuilder(par.FirstBlock);
		foreach (var andBuilder in andBuilders)
		{
			var andBlock = par.AddAndBlock();
			andBuilder(andBlock);
		}
		return sequence;
	}
	
	public static T Critical<T>(this T sequence, Action<SubSequenceBlock> criticalBuilder, params Action<SubSequenceBlock>[] optionBuilders)
		where T : SequenceBase
	{
		var par = sequence.CreateCritical();
		criticalBuilder(par.FirstBlock);
		foreach (var andBuilder in optionBuilders)
		{
			var andBlock = par.AddOptionBlock();
			andBuilder(andBlock);
		}
		return sequence;
	}

	public static T SetAutoNumbering<T>(this T sequence)
		where T : SequenceBase
	{
		sequence.TryAdd(new AutoNumber());
		return sequence;
	}
}