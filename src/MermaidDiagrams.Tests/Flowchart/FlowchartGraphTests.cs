using GwtUnit.XUnit;
using MermaidDiagrams.Flowchart;

namespace MermaidDiagrams.Tests.Flowchart;

[UsesVerify]
public sealed class FlowchartGraphTests : XUnitTestBase<FlowchartGraphTests.Thens>
{
	[Fact]
	public Task ShouldHaveValidFlowchart_WhenRendering()
	{
		When(Rendering);

		return Verifier.Verify(Then.Diagram);
	}

	[Fact]
	public Task ShouldHaveValidFlowchart_WhenUsingExampleA()
	{
		When(UsingExampleA, Rendering);

		return Verifier.Verify(Then.Diagram);
	}
	
	[Fact]
	public Task ShouldHaveValidFlowchart_WhenUsingExampleA_AddingClassDefs()
	{
		When(UsingExampleA, AddingClassDefinitions, Rendering);

		return Verifier.Verify(Then.Diagram);
	}
	
	[Fact]
	public Task ShouldHaveValidFlowchart_WhenUsingExampleB()
	{
		When(UsingExampleB, Rendering);

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

	private void UsingExampleA()
	{
		var flow = Then.Target;

		flow.AddDirective(new DirectiveInitialize(KnownThemes.Forest));
		flow.SetHeader(new Header("This is a test"));
		DiagramBaseExtensions.AddAnd(flow, new[] { new Comment("No comment") });

		flow.Node("A", "Hard edge", Shape.Box);
		flow.Node("B", "Round edge", Shape.RoundedBox);

		flow.AddLink(flow["A"], flow["B"], Edge.Arrow.WithLabel("Link text"));

		var c = flow.Node("C", "Decision", Shape.Rhombus);

		var d = flow.Node("D", "Result One", Shape.Trapezoid);

		var e = flow.Node("E", "Result Two", Shape.Circle);

		flow.AddLink(flow["B"], c, Edge.Arrow);
		flow.AddLink(c, d, Edge.Arrow.WithLabel("Yes"));
		flow.AddLink(c, e, Edge.Arrow.WithLabel("No"));
	}

	private void UsingExampleB()
	{
		var flow = Then.Target;

		flow.Subgraph("Outer", Identifier.Next("sg"), FlowDirection.TopBottom, sub =>
		{
			sub.Node("Something", Shape.Stadium);

			var sg = sub.Subgraph("Inner", "in1");
			sg.Node("A", "Hard edge", Shape.Box);
		});
	}

	private void AddingClassDefinitions()
	{
		var cd = Then.Target.GetClassDefinitions();

		cd.GetOrCreate("neat", s
			=> new ClassDef(s)
				.Style("fill", "#f96")
				.Style("stroke", "#333")
				.Style("stroke-width", "2px").Assign("A")
		);
	}

	public sealed class Thens
	{
		public FlowchartGraph Target { get; set; }
		public string Diagram { get; set; }
	}
}