namespace MermaidDiagrams.Flowchart;

public record FlowchartOptions : StandardOptions, IFlowchartOptions
{
	/// <summary>
	/// Always render nodes as stand-alone statements, even if they are part of an edge.
	/// </summary>
	public bool VerboseNodes { get; init; } = false;
}