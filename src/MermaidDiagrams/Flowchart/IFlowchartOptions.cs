using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public interface IFlowchartOptions : IRenderOptions
{
	bool VerboseNodes { get; init; }
}