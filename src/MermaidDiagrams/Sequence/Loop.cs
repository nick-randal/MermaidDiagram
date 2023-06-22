namespace MermaidDiagrams.Sequence;

public class Loop : SubSequence
{
	public Loop(Text label)
	{
		Label = label.IsEmpty ? null : label.Content;
	}

	public override string TypeName => "loop";
}