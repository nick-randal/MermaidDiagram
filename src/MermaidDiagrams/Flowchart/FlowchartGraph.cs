using MermaidDiagrams.Contracts;
using MermaidDiagrams.Support;

namespace MermaidDiagrams.Flowchart;

public class FlowchartGraph : FlowchartBase
{
	public FlowchartGraph(FlowDirection direction) : base(null, (TypeOfFlowchart)direction)
	{
	}

	public virtual FlowchartGraph SetOptions(FlowchartInit init)
	{
		AddDirective(new InitializeDirective<FlowchartInit>(init));
		return this;
	}
	
	public virtual FlowchartGraph AddDirective(IDirective directive)
	{
		Add(directive);
		return this;
	}

	public virtual FlowchartGraph SetHeader(Header header)
	{
		TryAdd(header);
		return this;
	}
}