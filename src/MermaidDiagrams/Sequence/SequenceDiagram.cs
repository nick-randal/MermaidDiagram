using MermaidDiagrams.Support;

namespace MermaidDiagrams.Sequence;

public class SequenceDiagram : SequenceBase
{
	public SequenceDiagram() : base(new DiagramType.Basic("sequenceDiagram"))
	{
	}
}