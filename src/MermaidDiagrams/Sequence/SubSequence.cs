using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public abstract class SubSequence : SequenceBase
{
	protected SubSequence() : base(new TypeOfNone())
	{
	}

	public abstract string TypeName { get; }
	
	public string? Label { get; protected set; }
	
	protected bool HasLabel => string.IsNullOrWhiteSpace(Label) is false;
	
	public override void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Append(TypeName);
		if(string.IsNullOrWhiteSpace(Label) is false)
			textBuilder.Append($" {Label}");
		textBuilder.Line();

		RenderRegularStatements(textBuilder, renderState);
		
		textBuilder.Line($"{renderState.Indent}end");
	}
}