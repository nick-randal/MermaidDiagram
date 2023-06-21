using MermaidDiagrams.Flowchart;

namespace MermaidDiagrams.Sequence;

public sealed class SequenceDiagram : DiagramBase
{
	public SequenceDiagram()
	{
		SetType(new TypeOfSequence());
	}
}