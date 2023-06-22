using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public class AlternateIfElse : LabeledSubSequenceBase
{
	private readonly bool _isIf;

	internal AlternateIfElse(bool isIf, Text label) : base(label)
	{
		_isIf = isIf;
	}	
	
	public override void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		if(_isIf is false)
			textBuilder.Append(renderState.Indent);
		
		textBuilder.Append(TypeName);
		if(string.IsNullOrWhiteSpace(TypeText) is false)
			textBuilder.Append($" {TypeText}");
		textBuilder.Line();

		RenderRegularStatements(textBuilder, renderState);
		
		if(_isIf is false)
			textBuilder.Line($"{renderState.Indent}end");
	}
	
	public override string TypeName => _isIf ? "alt" : "else";
}