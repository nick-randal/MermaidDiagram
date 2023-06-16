using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public class FlowchartGraph : FlowchartBase
{
	public FlowchartGraph(FlowDirection direction)
	{
		SetType((TypeOfFlowchart)direction);
	}

	public virtual string Render() => Render(new FlowchartOptions());

	public string Render(IFlowchartOptions options)
	{
		var state = new RenderState<IFlowchartOptions>(options);
		var builder = new TextBuilder();

		base.Render(builder, state);

		return builder.Text;
	}
}