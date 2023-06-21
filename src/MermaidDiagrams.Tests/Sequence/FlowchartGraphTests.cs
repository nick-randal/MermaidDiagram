using GwtUnit.XUnit;
using MermaidDiagrams.Flowchart;
using MermaidDiagrams.Sequence;

namespace MermaidDiagrams.Tests.Sequence;

[UsesVerify]
public sealed class SequenceDiagramTests : XUnitTestBase<SequenceDiagramTests.Thens>
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
	public Task ShouldHaveValidFlowchart_WhenUsingExampleB()
	{
		When(UsingExampleB, Rendering);

		return Verify(Then.Diagram);
	}

	protected override void Creating()
	{
		Then.Target = new SequenceDiagram();
	}

	private void Rendering()
	{
		Then.Diagram = Then.Target.Render();
	}

	private void UsingExampleA()
	{
		var sequence = Then.Target;

		sequence.Add(new Participant("Alice"));
		sequence.Add(new Participant("Bob"));
	}

	private void UsingExampleB()
	{
		var sequence = Then.Target;
		
	}

	public sealed class Thens
	{
		public SequenceDiagram Target { get; set; }
		public string Diagram { get; set; }
	}
}