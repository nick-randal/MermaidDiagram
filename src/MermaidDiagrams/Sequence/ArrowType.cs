namespace MermaidDiagrams.Sequence;

/// <summary>
/// ->	Solid line without arrow
/// -->	Dotted line without arrow
/// ->>	Solid line with arrowhead
/// -->>	Dotted line with arrowhead
/// -x	Solid line with a cross at the end
/// --x	Dotted line with a cross at the end.
/// -)	Solid line with an open arrow at the end (async)
/// --)	Dotted line with a open arrow at the end (async)
/// </summary>
public enum ArrowType
{
	[Display(ShortName = "->")]
	SolidLine,
	
	[Display(ShortName = "-->")]
	DottedLine,
	
	[Display(ShortName = "->>")]
	SolidLineArrow,
	
	[Display(ShortName = "-->>")]
	DottedLineArrow,
	
	[Display(ShortName = "-x")]
	SolidLineCross,
	
	[Display(ShortName = "--x")]
	DottedLineCross,
	
	[Display(ShortName = "-)")]
	SolidLineOpenArrow,
	
	[Display(ShortName = "--)")]
	DottedLineOpenArrow
}