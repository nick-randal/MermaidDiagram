using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

using This = ISubgraph;

public interface ISubgraph : IStatement//, IFlowchartGraph todo
{
	Text Label { get; }
	
	string? Id { get; }

	This SetDirection(FlowDirection direction);
}

public class Subgraph : FlowchartGraph, ISubgraph
{
	public Subgraph(Text label, string? id = null)
	{
		Label = label;
		Id = id;
	}

	public Text Label { get; }
	
	public string? Id { get; }
	
	public This SetDirection(FlowDirection direction)
	{
		Statements.Add(new Literal($"direction {direction.GetShortName()}"));
		return this;
	}
	
	public override void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line(string.IsNullOrWhiteSpace(Id) ? $"subgraph {Label}" : $"subgraph {Id} [{Label}]");

		RenderStatements(textBuilder, renderState);
		
		textBuilder.Line("end");
	}

	public override void SetHeader(Header header) => throw new NotSupportedException();

	public override void SetType(IDiagramType type) => throw new NotSupportedException();
}