namespace MermaidDiagrams.Sequence;

public class Alt : LabeledSubSequenceBase
{
	public Alt(Text label) : base(label)
	{
		Else = new AltElse(new Text("else"));
	}

	public AltElse Else { get; }

	public override string TypeName => "alt";
}