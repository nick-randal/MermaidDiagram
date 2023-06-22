using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public class Alternate : SubSequence
{
	public Alternate(Text ifLabel, Text elseLabel)
	{
		IfBlock = new AlternateIfElse(true, ifLabel);
		ElseBlock = new AlternateIfElse(false, elseLabel);
	}

	public AlternateIfElse IfBlock { get; set; }

	public AlternateIfElse ElseBlock { get; }

	public override string TypeName => "";

	public override void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		IfBlock.Render(textBuilder, renderState);
		ElseBlock.Render(textBuilder, renderState);
	}
}