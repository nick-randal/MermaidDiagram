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
}