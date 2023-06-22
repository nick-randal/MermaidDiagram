using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public abstract class SubSequenceBlocks<T> : SubSequence
	where T: SubSequenceBlock
{
	public override void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		RenderGroup<T>(textBuilder, renderState);
		textBuilder.Line($"{renderState.Indent}end");
	}

	public override string TypeName => throw new NotSupportedException();

	protected T AddBlock(T block)
	{
		Blocks.Add(block);
		Add(block);
		return block;
	}

	protected T this[int index] => Blocks[index];

	protected readonly List<T> Blocks = new ();
}