namespace MermaidDiagrams.Sequence;

public class Highlight : SubSequence
{
	public Highlight(Rgb color)
	{
		Background = color;
		Label = Background.ToString();
	}

	public Rgb Background { get; }

	public override string TypeName => "rect";
}