namespace MermaidDiagrams.Sequence;

public class Loop : LabeledSubSequenceBase
{
	public Loop(Text label) : base(label)
	{
	}

	public override string TypeName => "loop";
}