using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public abstract class SubSequence : SequenceBase
{
	protected SubSequence() : base(new TypeOfNone())
	{
	}

	public abstract string TypeName { get; }
	
	public string? TypeText { get; protected set; }
	
	public override void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Append(TypeName);
		if(string.IsNullOrWhiteSpace(TypeText) is false)
			textBuilder.Append($" {TypeText}");
		textBuilder.Line();

		RenderRegularStatements(textBuilder, renderState);
		
		textBuilder.Line($"{renderState.Indent}end");
	}
}