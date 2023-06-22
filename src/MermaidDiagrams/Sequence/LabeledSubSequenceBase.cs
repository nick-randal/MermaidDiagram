namespace MermaidDiagrams.Sequence;

public abstract class LabeledSubSequenceBase : SubSequence
{
	protected LabeledSubSequenceBase(Text label)
	{
		Label = label;
		if(Label.IsEmpty is false)
			TypeText = Label.Content;
	}

	public Text Label { get; }
}