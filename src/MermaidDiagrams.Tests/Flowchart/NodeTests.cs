using FluentAssertions;
using GwtUnit.XUnit;
using MermaidDiagrams.Flowchart;
using MermaidDiagrams.Support;

namespace MermaidDiagrams.Tests.Flowchart;

public sealed class NodeTests : XUnitTestBase<NodeTests.Thens>
{
	[Fact]
	public void ShouldHaveValidNode_WhenCreating()
	{
		When(Creating, Rendering);

		Then.Text.Should().Be("bob{{Bob}}\r\n");
	}
	
	[Fact]
	public void ShouldHaveValidNode_WhenCreatingInvisible()
	{
		When(NotCreating, CreatingInvisible, Rendering);

		Then.Text.Should().Be("bob[ ]\r\n");
	}
	
	protected override void Creating()
	{
		Then.Target = new Node("bob", "Bob", Shape.Hexagon);
	}
	
	private void CreatingInvisible()
	{
		Then.Target = Node.CreateInvisible("bob");
	}

	private void Rendering()
	{
		var text = new TextBuilder();
		Then.Target.Render(text, new RenderState());
		Then.Text = text.Text;
	}

	public class Thens
	{
		public INode Target { get; set; }
		public string Text { get; set; }
	}
}