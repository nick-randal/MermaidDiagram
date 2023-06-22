using System.Collections.Immutable;

namespace MermaidDiagrams.Sequence;

public class Critical : SubSequenceBlocks<SubSequenceBlock>
{
	public Critical()
	{
		FirstBlock = new("critical", true);
		Add(FirstBlock);
	}
	
	public SubSequenceBlock FirstBlock { get; }
	
	public ImmutableList<SubSequenceBlock> OptionBlocks => Blocks.Skip(1).ToImmutableList();

	public SubSequenceBlock AddOptionBlock() => AddBlock(new SubSequenceBlock("option", false));
}

