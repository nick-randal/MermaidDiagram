namespace MermaidDiagrams.Flowchart;

public enum FlowDirection
{
	[Display(ShortName = "TB")]
	TopBottom,
	
	[Display(ShortName = "TD")]
	TopDown,
	
	[Display(ShortName = "BT")]
	BottomTop,
		
	[Display(ShortName = "LR")]
	LeftRight,
	
	[Display(ShortName = "RL")]
	RightLeft
}