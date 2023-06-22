using System.Collections.Immutable;

namespace MermaidDiagrams.Sequence;

public class Parallel : SubSequenceBlocks<SubSequenceBlock>
{
	public Parallel()
	{
		FirstBlock = new("par", true);
		Add(FirstBlock);
	}
	
	public SubSequenceBlock FirstBlock { get; }
	
	public ImmutableList<SubSequenceBlock> AndBlocks => Blocks.Skip(1).ToImmutableList();

	public SubSequenceBlock AddAndBlock() => AddBlock(new SubSequenceBlock("and", false));
}