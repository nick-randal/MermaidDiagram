using System.Collections.Concurrent;

namespace MermaidDiagrams.Flowchart;

public record Edge(LineStyle Line = LineStyle.Normal, EndStyle End = EndStyle.Open, ushort Depth = 1)
{
	public override int GetHashCode()
	{
		return ((int)Line << 24) | ((int)End << 16) | Depth;
	}

	public Edge WithLabel(Text? label)
	{
		return this with { Label = label ?? Text.Empty };
	}

	public Text Label { get; private init; } = Text.Empty;

	public override string ToString()
	{
		if (Line == LineStyle.Invisible)
			return $"~~~{(Label.IsEmpty ? string.Empty : $"|{Label}|")}";

		var key = GetHashCode();
		if (Cache.TryGetValue(key, out var edge) is false)
		{
			var (outerChar, innerChar, adj) = Line switch
			{
				LineStyle.Thick => ('=', '=', 1),
				LineStyle.Dotted => (' ', '.', 0),
				_ => ('-', '-', 1)
			};

			var (tailChar, headChar, count) = End switch
			{
				EndStyle.Arrow => (outerChar, '>', Depth),
				EndStyle.ArrowBoth => ('<', '>', Depth + adj),
				EndStyle.X => ('x', 'x', Depth + adj),
				EndStyle.O => ('o', 'o', Depth + adj),
				_ => (outerChar, outerChar, Depth)
			};

			edge = Line is LineStyle.Dotted
				? $"{tailChar}-{new string(innerChar, count)}-{headChar}".Trim()
				: $"{tailChar}{new string(innerChar, count)}{headChar}";
			
			Cache.TryAdd(key, edge);
		}
		
		return $"{edge}{(Label.IsEmpty ? string.Empty : $"|{Label}|")}";
	}
	
	private static readonly ConcurrentDictionary<int, string> Cache = new();
	
	public static readonly Edge Invisible = new(LineStyle.Invisible);
	
	public static readonly Edge Open = new();
	
	public static readonly Edge Arrow = new(LineStyle.Normal, EndStyle.Arrow);
	
	public static readonly Edge ArrowBoth = new(LineStyle.Normal, EndStyle.ArrowBoth);
}