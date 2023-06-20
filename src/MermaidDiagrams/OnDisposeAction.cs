namespace MermaidDiagrams;

public record OnDisposeAction : IDisposable
{
	internal OnDisposeAction(Action act)
	{
		_act = act;
	}

	public void Dispose()
	{
		_act();
	}
	
	private readonly Action _act;
}