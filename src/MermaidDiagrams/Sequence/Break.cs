namespace MermaidDiagrams.Sequence;

public class Break : SubSequence
{
	public Break(Text label)
	{
		Label = label.IsEmpty ? null : label.Content;
	}

	public override string TypeName => "break";
}