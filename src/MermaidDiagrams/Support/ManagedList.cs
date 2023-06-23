using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Support;

internal enum ItemChange
{
	Added,
	Removed
}

internal class ManagedList<T>
{
	public ManagedList(Action<T, ItemChange>? onItemChange = null)
	{
		_onItemChange = onItemChange;
	}
	
	public T Add(T item)
	{
		_items.Add(item);
		_onItemChange?.Invoke(item, ItemChange.Added);
		return item;
	}

	public TF? FirstOrDefaultOf<TF>(TF? def = default)
	{
		return _items.OfType<TF>().FirstOrDefault() ?? def;
	}

	public IEnumerable<TF> OfType<TF>()
	{
		return _items.OfType<TF>();
	}

	public TF SingleOf<TF>()
		where TF : IRenderable
	{
		return _items.OfType<TF>().Single();
	}

	public T Single(Func<T, bool> func)
	{
		return _items.Single(func);
	}

	private readonly Action<T,ItemChange>? _onItemChange;
	private readonly List<T> _items = new();

	public bool Remove(T existing)
	{
		var removed = _items.Remove(existing);
		if(removed)
			_onItemChange?.Invoke(existing, ItemChange.Removed);
		return removed;
	}

	public void Insert(int i, T item)
	
	{
		_items.Insert(i, item);
		_onItemChange?.Invoke(item, ItemChange.Added);
	}

	public void AddRange(T[] renderables)
	{
		_items.AddRange(renderables);
		if (_onItemChange is null)
			return;
		
		foreach (var renderable in renderables)
		{
			_onItemChange.Invoke(renderable, ItemChange.Added);
		}
	}
}