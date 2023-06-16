using System.Collections.Concurrent;

namespace MermaidDiagrams.Flowchart;

public record Edge(LineStyle Line = LineStyle.Normal, EndStyle End = EndStyle.Open, int Depth = 1)
{
	public override string ToString()
	{
		if (Line == LineStyle.Invisible)
			return "~~~";

		var key = ((uint)Line << 26) | ((uint)End << 25) | ((uint)Line << 19) | (uint)Depth;
		if (Cache.TryGetValue(key, out var edge))
			return edge;

		var (outerChar, innerChar, adj) = Line switch
		{
			LineStyle.Thick => ('=', '=', 1),
			LineStyle.Dotted => (' ', '.', 0),
			_ => ('-', '-', 1)
		};
		
		var (tailChar, headChar, count) = End switch
		{
			EndStyle.Arrow => (outerChar, '>', Depth),
			EndStyle.ArrowBoth => ('<', '>', Depth+adj),
			EndStyle.X => ('x', 'x', Depth+adj),
			EndStyle.O => ('o', 'o', Depth+adj),
			_ =>  (outerChar, outerChar, Depth)
		};
		
		edge = Line is LineStyle.Dotted 
			? $"{tailChar}-{new string(innerChar, count)}-{headChar}".Trim()
			: $"{tailChar}{new string(innerChar, count)}{headChar}";

		Cache.TryAdd(key, edge);
		return edge;
	}
	
	private static readonly ConcurrentDictionary<uint, string> Cache = new();
	
	public static readonly Edge Invisible = new Edge(LineStyle.Invisible);
	
	public static readonly Edge Open = new Edge(LineStyle.Normal, EndStyle.Open);
	
	public static readonly Edge Arrow = new Edge(LineStyle.Normal, EndStyle.Arrow);
	
	public static readonly Edge ArrowBoth = new Edge(LineStyle.Normal, EndStyle.ArrowBoth);
}