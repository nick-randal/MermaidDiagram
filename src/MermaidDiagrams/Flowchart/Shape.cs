namespace MermaidDiagrams.Flowchart;

public enum Shape
{
	[Display(ShortName = "[ ]")]
	Box = 0,
	
	[Display(ShortName = "( )")]
	RoundedBox = 1,
	
	[Display(ShortName = "([ ])")]
	Stadium = 2,
	
	[Display(ShortName = "[[ ]]")]
	Subroutine = 3,
	
	[Display(ShortName = "[( )]")]
	Cylinder = 4,
	
	[Display(ShortName = "(( ))")]
	Circle = 5,
	
	[Display(ShortName = "{ }")]
	Rhombus = 6,
	
	[Display(ShortName = "> ]")]
	BannerLeft = 7,
	
	[Display(ShortName = "{{ }}")]
	Hexagon = 8,
	
	[Display(ShortName = "[\\ \\]")]
	Parallelogram = 9,
	
	[Display(ShortName = "[/ \\]")]
	Trapezoid = 10,
	
	[Display(ShortName = "[\\ /]")]
	TrapezoidAlt = 11,
	
	[Display(ShortName = "((( )))")]
	DoubleCircle = 12,
}