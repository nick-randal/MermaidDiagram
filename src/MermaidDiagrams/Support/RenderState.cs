using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

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

	public TState State<TState>() where TState : Enum => (_states.Count is 0 ? default(TState) : (TState)_states.Peek())!;

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

	public OnDisposeAction PushState<TState>(TState state)
		where TState : Enum
	{
		_states.Push(state);
		return new OnDisposeAction(PopState);
	}

	private void PopState()
	{
		if(_states.Count > 0)
			_states.Pop();
	}

	private readonly Stack<Enum> _states = new();
	private int _depth;
	private readonly object _lock = new();
	private readonly List<string> _indents = new();
}