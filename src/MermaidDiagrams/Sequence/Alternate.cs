using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public class Alternate : SubSequence
{
	public Alternate()
	{
		IfBlock = new SubSequenceBlock("alt", true);
		ElseBlock = new SubSequenceBlock("else", false);
		AddRange(IfBlock, ElseBlock);
	}
	
	public Alternate(Text ifLabel, Text elseLabel)
	{
		IfBlock = new SubSequenceBlock("alt", ifLabel, true);
		ElseBlock = new SubSequenceBlock("else", elseLabel, false);
		AddRange(IfBlock, ElseBlock);
	}

	public SubSequenceBlock IfBlock { get; }

	public SubSequenceBlock ElseBlock { get; }

	public override string TypeName => "";

	public override void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		RenderGroup<SubSequenceBlock>(textBuilder, renderState);
		textBuilder.Line($"{renderState.Indent}end");
	}
}