using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Support;

public record RenderState(string IndentValue = "  ") : IRenderState
{
	public int Depth => _depth;

	public string Indent
	{
		get
		{
			if(_depth < _indents.Count)
				return _indents[_depth];
			
			lock(_lock)
			{
				while(_depth >= _indents.Count)
					_indents.Add(string.Join(string.Empty, Enumerable.Repeat(IndentValue, _depth)));
			}
			return _indents[_depth];
		}
	}

	public OnDisposeAction StepIn()
	{
		_depth++;
		return new OnDisposeAction(StepOut);
	}

	public void StepOut()
	{
		if(_depth > 0)
			_depth--;
	}
	
	private int _depth;
	private readonly object _lock = new();
	private readonly List<string> _indents = new();
}