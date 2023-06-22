using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public class SubSequenceBlock : SubSequence
{
	internal SubSequenceBlock(string typeName, bool primary)
	{
		_primary = primary;
		TypeName = typeName;
	}
		
	internal SubSequenceBlock(string typeName, Text label, bool primary) : this(typeName, primary)
	{
		SetLabel(label);
	}
	
	public SubSequenceBlock SetLabel(Text label)
	{
		Label = label.IsEmpty ? null : label.Content;
		return this;
	}

	public override void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		if(_primary is false)
			textBuilder.Append(renderState.Indent);
		
		textBuilder.Append(TypeName);
		if(HasLabel)
			textBuilder.Append($" {Label}");
		textBuilder.Line();

		RenderRegularStatements(textBuilder, renderState);
	}
	
	public override string TypeName { get; }
	
	private readonly bool _primary;
}