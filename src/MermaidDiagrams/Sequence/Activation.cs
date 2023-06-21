using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Sequence;

public record Activation(Identifier Id, bool Activate = true) : IStatement, IIdentifiable
{
	public Activation(IIdentifiable item, bool activate = true) : this(item.Id, activate)
	{
	}

	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Append(Activate ? "activate " : "deactivate ");
		textBuilder.Line(Id.ToString());
	}
}