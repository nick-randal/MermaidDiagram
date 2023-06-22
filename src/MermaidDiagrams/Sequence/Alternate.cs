namespace MermaidDiagrams.Sequence;

public class Alternate : SubSequenceBlocks<SubSequenceBlock>
{
	public Alternate()
		: this(
			new SubSequenceBlock("alt", true),
			new SubSequenceBlock("else", false)
		)
	{
	}

	public Alternate(Text ifLabel, Text elseLabel)
		: this(
			new SubSequenceBlock("alt", ifLabel, true),
			new SubSequenceBlock("else", elseLabel, false)
		)
	{
	}

	protected Alternate(SubSequenceBlock ifBlock, SubSequenceBlock elseBlock)
	{
		IfBlock = AddBlock(ifBlock);
		ElseBlock = AddBlock(elseBlock);
	}

	public SubSequenceBlock IfBlock { get; }

	public SubSequenceBlock ElseBlock { get; }
}