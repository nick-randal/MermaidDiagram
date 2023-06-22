namespace MermaidDiagrams.Sequence;

public class Optional : LabeledSubSequenceBase
{
	public Optional(Text label) : base(label)
	{
	}
	
	public override string TypeName => "opt";
}