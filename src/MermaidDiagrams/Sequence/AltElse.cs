namespace MermaidDiagrams.Sequence;

public class AltElse : LabeledSubSequenceBase
{
	internal AltElse(Text label) : base(label)
	{
	}	
	
	public override string TypeName => "else";
}