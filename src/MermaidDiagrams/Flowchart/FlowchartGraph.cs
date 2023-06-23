using MermaidDiagrams.Contracts;
using MermaidDiagrams.Support;

namespace MermaidDiagrams.Flowchart;

public class FlowchartGraph : FlowchartBase
{
	public FlowchartGraph(FlowDirection direction) : base(null, (TypeOfFlowchart)direction)
	{
	}

	public virtual FlowchartGraph SetTheme(string theme)
	{
		AddDirective(new InitializeDirective<FlowchartInit>(new FlowchartInit {Theme = theme}));
		return this;
	}

	public virtual FlowchartGraph SetTheme(ThemeVariables customTheme)
	{
		AddDirective(new InitializeDirective<FlowchartInit>(new FlowchartInit { Theme = "custom", ThemeVariables = customTheme}));
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