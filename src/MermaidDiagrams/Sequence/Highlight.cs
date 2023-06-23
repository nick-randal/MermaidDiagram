namespace MermaidDiagrams.Sequence;

public class Highlight : SubSequence
{
	public Highlight(Rgb color)
	{
		Background = color;
		Label = Background.ToString(RgbFormat.Rgb);
	}

	public Rgb Background { get; }

	public override string TypeName => "rect";
}