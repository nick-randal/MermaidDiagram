using MermaidDiagrams.Contracts;
using MermaidDiagrams.Support;

namespace MermaidDiagrams.Flowchart;

internal record TypeOfFlowchart(FlowDirection Direction) : IDiagramType
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"{Name} {Direction.GetShortName()}");
	}
	
	public string Name => "flowchart";
	
	public static explicit operator TypeOfFlowchart(FlowDirection direction) => new(direction);
}