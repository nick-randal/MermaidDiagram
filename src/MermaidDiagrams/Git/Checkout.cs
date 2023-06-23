using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Git;

public interface ICheckout : IStatement
{
	string Name { get; }
}

public record Checkout(string Name) : ICheckout
{
	public Checkout(IBranch branch) : this(branch.Name)
	{
	}
	
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"checkout \"{Name}\"");
	}
}