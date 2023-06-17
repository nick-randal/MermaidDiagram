using GwtUnit.XUnit;
using MermaidDiagrams.Flowchart;

namespace MermaidDiagrams.Tests;

[UsesVerify]
public sealed class FlowchartGraphTests : XUnitTestBase<FlowchartGraphTests.Thens>
{
	[Fact]
	public Task ShouldHaveValidFlowchart_WhenRendering()
	{
		When(Rendering);
		
		return Verifier.Verify(Then.Diagram);
	}

	protected override void Creating()
	{
		Then.Target = new FlowchartGraph(FlowDirection.LeftRight);
	}

	private void Rendering()
	{
		Then.Diagram = Then.Target.Render();
	}

	public class Thens
	{
		public FlowchartGraph Target { get; set; }
		public string Diagram { get; set; }
	}
}