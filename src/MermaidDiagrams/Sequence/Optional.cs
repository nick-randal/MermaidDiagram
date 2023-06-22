namespace MermaidDiagrams.Sequence;

public class Optional : SubSequence
{
	public Optional(Text label)
	{
		Label = label.IsEmpty ? null : label.Content;
	}
	
	public override string TypeName => "opt";
}