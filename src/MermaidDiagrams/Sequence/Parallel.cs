using System.Collections.Immutable;
using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public class Parallel : SubSequence
{
	public Parallel()
	{
		FirstBlock = new("par", true);
		Add(FirstBlock);
	}
	
	public SubSequenceBlock FirstBlock { get; }
	
	public SubSequenceBlock AddAndBlock()
	{
		var block = new SubSequenceBlock("and", false);
		_ands.Add(block);
		Add(block);
		return block;
	}
	
	public ImmutableList<SubSequenceBlock> AndBlocks => _ands.ToImmutableList();
	
	public override string TypeName => "par";

	public override void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		RenderGroup<SubSequenceBlock>(textBuilder, renderState);
		textBuilder.Line($"{renderState.Indent}end");
	}
	
	private readonly List<SubSequenceBlock> _ands = new ();
}