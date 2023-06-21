using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public class FlowchartGraph : FlowchartBase
{
	public FlowchartGraph(FlowDirection direction) : base(null, (TypeOfFlowchart)direction)
	{
	}

	public virtual FlowchartGraph SetTheme(string theme)
	{
		AddDirective(new DirectiveInitialize(theme));
		return this;
	}

	public virtual FlowchartGraph SetTheme(ThemeVariables customTheme)
	{
		AddDirective(new DirectiveInitialize(customTheme));
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

	public virtual string Render()
	{
		var state = new RenderState();
		var builder = new TextBuilder();

		base.Render(builder, state);

		return builder.Text;
	}
}