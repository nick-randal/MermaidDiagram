namespace MermaidDiagrams;

public record Identifier
{
	public Identifier(string? id)
	{
		Value = id?.Trim() ?? Next();
		NoId = string.IsNullOrWhiteSpace(Value);
	}

	public void Deconstruct(out string Value, out bool NoId)
	{
		Value = this.Value;
		NoId = this.NoId;
	}

	public string Value { get; }

	public bool NoId { get; }

	public override string ToString() => Value;

	public virtual bool Equals(Identifier? other)
	{
		if (ReferenceEquals(null, other))
			return false;
		if (ReferenceEquals(this, other)) 
			return true;
		
		return Value.Equals(other.Value);
	}

	public static implicit operator Identifier(string? id) => new(id ?? Next());

	public static string Next(string prefix = "id") => $"{prefix}{Interlocked.Increment(ref _idCounter)}";

	private static long _idCounter = 0;
}