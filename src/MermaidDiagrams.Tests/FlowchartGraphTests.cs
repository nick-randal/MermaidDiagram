﻿using GwtUnit.XUnit;
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
	
	[Fact]
	public Task ShouldHaveValidFlowchart_WhenUsingExampleA()
	{
		When(UsingExampleA, Rendering);
		
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
		flow.Add(new Comment("No comment"));

		flow.Node("A", "Hard edge", Shape.Box);
		flow.Node("B", "Round edge", Shape.RoundedBox);

		flow.Link(flow["A"], flow["B"], Edge.Arrow.WithLabel("Link text"));

		var c = flow.Node("C", "Decision", Shape.Rhombus);
		
		var d = flow.Node("D", "Result One", Shape.Trapezoid);
		
		var e = flow.Node("E", "Result Two", Shape.Circle);

		flow.Link(flow["B"], c, Edge.Arrow);
		flow.Link(c, d, Edge.Arrow.WithLabel("Yes"));
		flow.Link(c, e, Edge.Arrow.WithLabel("No"));
	}
	
	public class Thens
	{
		public FlowchartGraph Target { get; set; }
		public string Diagram { get; set; }
	}
}