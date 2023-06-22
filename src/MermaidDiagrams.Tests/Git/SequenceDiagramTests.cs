using System.Drawing;
using GwtUnit.XUnit;
using MermaidDiagrams.Git;
using MermaidDiagrams.Sequence;

namespace MermaidDiagrams.Tests.Git;

[UsesVerify]
public sealed class GitGraphTests : XUnitTestBase<GitGraphTests.Thens>
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

	protected override void Creating()
	{
		Then.Target = new GitGraph();
	}

	private void Rendering()
	{
		Then.Diagram = Then.Target.Render();
	}

	private void UsingExampleA()
	{
		var git = Then.Target;

		git.Commit();
	}

	public sealed class Thens
	{
		public GitGraph Target { get; set; }
		public string Diagram { get; set; }
	}
}