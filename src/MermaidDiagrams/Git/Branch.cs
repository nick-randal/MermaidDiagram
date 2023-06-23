using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Git;

public interface IBranch : IStatement
{
	string Name { get; }
}

public record Branch(string Name) : IBranch
{
	public Branch(string Name, Order order) : this(Name)
	{
		Order = order;
	}
	
	public Order Order { get; } = Order.None;
	
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Append($"branch \"{Name}\"");
		if(Order.NoValue is false)
			textBuilder.Append($" order: {Order.Value}");
		textBuilder.Line();
	}
}