namespace MermaidDiagrams;

public record StateInline : IDisposable
{
	private readonly Action _act;

	internal StateInline(Action act)
	{
		_act = act;

	}

	public void Dispose()
	{
		_act();
	}
}