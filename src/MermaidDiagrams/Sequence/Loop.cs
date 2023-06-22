namespace MermaidDiagrams.Sequence;

public class Loop : SubSequence
{
	public Loop(Text label) : base("loop", label.IsEmpty ? null : label.Content)
	{
		Label = label;
	}

	public Text Label { get; }
}