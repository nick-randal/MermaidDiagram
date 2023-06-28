using System.Drawing;
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

		return Verify(Then.Diagram);
	}

	[Fact]
	public Task ShouldHaveValidFlowchart_WhenUsingExampleA()
	{
		When(UsingExampleA, Rendering);

		return Verify(Then.Diagram);
	}

	[Fact]
	public Task ShouldHaveValidFlowchart_WhenUsingExampleA_AddingClassDefs()
	{
		When(UsingExampleA, AddingClassDefinitions, Rendering);

		return Verify(Then.Diagram);
	}

	[Fact]
	public Task ShouldHaveValidFlowchart_WhenUsingExampleB()
	{
		When(UsingExampleB, Rendering);

		return Verify(Then.Diagram);
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

		var idA = "A".ToIdentifier();
		
		flow.SetOptions(new FlowchartInit { Theme = KnownThemes.Forest });
		flow.SetHeader(new Header("This is a test"));
		flow.AddAnd(new Comment("No comment"));

		flow.CreateNode(idA, "Hard edge", Shape.Box);
		flow.CreateNode("B", "Round edge", Shape.RoundedBox);

		flow.Link(idA, flow["B"], Edge.Arrow.WithLabel("Link text"));

		var c = flow.CreateNode("C", "Decision", Shape.Rhombus);

		var d = flow.CreateNode("D", "Result One", Shape.Trapezoid);

		var e = flow.CreateNode("E", "Result Two", Shape.Circle);

		flow.Link(flow["B"], c, Edge.Arrow);
		flow.Link(c, d, Edge.Arrow.WithLabel("Yes"));
		flow.Link(c, e, Edge.Arrow.WithLabel("No"));
	}

	private void UsingExampleB()
	{
		var flow = Then.Target;

		var customTheme = FlowchartInit.CreateCustomTheme(theme =>
		{
			theme.PrimaryColor = Color.SteelBlue;
			theme.SecondaryColor = Color.DarkGreen;
			theme.LineColor = "#363";
			theme.TertiaryColor = 0xf09060ff;
			theme.PrimaryBorderColor = Color.LightBlue;
			theme.PrimaryTextColor = Color.DarkGray;
		});
		
		flow.SetOptions(customTheme);

		flow.Subgraph("Outer", Identifier.Next("sg"), FlowDirection.TopBottom, sub =>
		{
			sub.CreateNode("Something", Shape.Stadium);

			var sg = sub.CreateSubgraph("Inner", "in1");
			sg.CreateNode("A", "A Box", Shape.Box);
			sg.CreateNode("B", "A Box", Shape.Box);
			sg.Link("A", "B", Edge.Arrow.WithLabel("A to B").WithDepth(7));

			sg.CreateInvisible("can'tSeeme");
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