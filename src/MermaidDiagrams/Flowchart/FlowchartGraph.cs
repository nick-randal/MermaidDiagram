namespace MermaidDiagrams.Flowchart;

public class FlowchartGraph : FlowchartBase
{
	public FlowchartGraph(FlowDirection direction)
	{
		SetType((TypeOfFlowchart)direction);
	}

	public FlowchartGraph SetTheme(string theme)
	{
		AddDirective(new DirectiveInitialize(KnownThemes.Forest));
		return this;
	}

	public FlowchartGraph SetTheme(ThemeVariables customTheme)
	{
		AddDirective(new DirectiveInitialize(customTheme));
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