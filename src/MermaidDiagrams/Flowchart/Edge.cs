using System.Collections.Concurrent;

namespace MermaidDiagrams.Flowchart;

public record Edge(LineStyle Line = LineStyle.Normal, EndStyle End = EndStyle.Open, int Depth = 1)
{
	public override string ToString()
	{
		if (Line == LineStyle.Invisible)
			return "~~~";

		//uint code = (Bidirectional ? (uint)0x8000_0000 : 0) | ((int)End << 25) | ((int)Line << 19) | Depth;
		// todo perform lookup
		
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
		
		return Line is LineStyle.Dotted 
			? $"{tailChar}-{new string(innerChar, count)}-{headChar}".Trim()
			: $"{tailChar}{new string(innerChar, count)}{headChar}";
	}
	
	//private static readonly ConcurrentDictionary<uint, string>
}