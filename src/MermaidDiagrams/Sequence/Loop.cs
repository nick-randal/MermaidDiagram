namespace MermaidDiagrams.Sequence;

public class Loop : SubSequence
{
	public Loop(string? label) : base("loop", label)
	{
		Label = label;
	}

	public string? Label { get; }
}