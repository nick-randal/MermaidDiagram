using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public abstract class SubSequence : SequenceBase
{
	protected SubSequence(string typeName, string? typeText) : base(new TypeOfNone())
	{
		TypeName = typeName;
		TypeText = typeText;
	}

	public string TypeName { get; }
	
	public string? TypeText { get; }

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

public class Alt : SubSequence
{
	public Alt() : base("alt", null)
	{
	}
}

public class Opt : SubSequence
{
	public Opt() : base("opt", null)
	{
	}
}

public class Par : SubSequence
{
	public Par() : base("par", null)
	{
	}
}

public class Critical : SubSequence
{
	public Critical() : base("critical", null)
	{
	}
}