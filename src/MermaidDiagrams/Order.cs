namespace MermaidDiagrams;

public record Order(int Value)
{
	public Order() : this(0)
	{
		NoValue = true;
	}	
	
	public bool NoValue { get; private init; }
	
	public static implicit operator Order(int value) => new(value);
	
	public static implicit operator Order((int value, bool noValue) t) => new(t.value) { NoValue = t.noValue };
	
	public static Order None { get; } = new();
}