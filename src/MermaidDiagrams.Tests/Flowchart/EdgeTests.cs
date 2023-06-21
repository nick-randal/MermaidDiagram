using MermaidDiagrams.Flowchart;

namespace MermaidDiagrams.Tests.Flowchart;

public sealed class EdgeTests
{
	[Fact]
	public void ShouldHaveEdge_WhenCreating_GivenDefaults()
	{
		// Arrange
		var edge = new Edge();
		
		// Act
		var actual = edge.ToString();
		
		// Assert
		Assert.Equal("---", actual);
	}

	[Theory,
	 InlineData(LineStyle.Invisible, EndStyle.Arrow, 123, "~~~"),
	 InlineData(LineStyle.Normal, EndStyle.Open, 1, "---"),
	 InlineData(LineStyle.Normal, EndStyle.Open, 3, "-----"),
	 InlineData(LineStyle.Normal, EndStyle.ArrowBoth, 1, "<-->"),
	 InlineData(LineStyle.Normal, EndStyle.ArrowBoth, 2, "<--->"),
	 InlineData(LineStyle.Normal, EndStyle.Arrow, 1, "-->"),
	 InlineData(LineStyle.Normal, EndStyle.Arrow, 3, "---->"),
	 InlineData(LineStyle.Normal, EndStyle.X, 1, "x--x"),
	 InlineData(LineStyle.Normal, EndStyle.X, 2, "x---x"),
	 InlineData(LineStyle.Normal, EndStyle.O, 1, "o--o"),
	 InlineData(LineStyle.Normal, EndStyle.O, 3, "o----o"),
	 InlineData(LineStyle.Thick, EndStyle.Open, 1, "==="),
	 InlineData(LineStyle.Thick, EndStyle.Open, 3, "====="),
	 InlineData(LineStyle.Thick, EndStyle.ArrowBoth, 1, "<==>"),
	 InlineData(LineStyle.Thick, EndStyle.ArrowBoth, 2, "<===>"),
	 InlineData(LineStyle.Thick, EndStyle.Arrow, 1, "==>"),
	 InlineData(LineStyle.Thick, EndStyle.Arrow, 3, "====>"),
	 InlineData(LineStyle.Thick, EndStyle.X, 1, "x==x"),
	 InlineData(LineStyle.Thick, EndStyle.X, 2, "x===x"),
	 InlineData(LineStyle.Thick, EndStyle.O, 1, "o==o"),
	 InlineData(LineStyle.Thick, EndStyle.O, 3, "o====o"),
	 InlineData(LineStyle.Dotted, EndStyle.Open, 1, "-.-"),
	 InlineData(LineStyle.Dotted, EndStyle.Open, 3, "-...-"),
	 InlineData(LineStyle.Dotted, EndStyle.ArrowBoth, 1, "<-.->"),
	 InlineData(LineStyle.Dotted, EndStyle.ArrowBoth, 2, "<-..->"),
	 InlineData(LineStyle.Dotted, EndStyle.Arrow, 1, "-.->"),
	 InlineData(LineStyle.Dotted, EndStyle.Arrow, 3, "-...->"),
	 InlineData(LineStyle.Dotted, EndStyle.X, 1, "x-.-x"),
	 InlineData(LineStyle.Dotted, EndStyle.X, 2, "x-..-x"),
	 InlineData(LineStyle.Dotted, EndStyle.O, 1, "o-.-o"),
	 InlineData(LineStyle.Dotted, EndStyle.O, 3, "o-...-o"),
	]
	public void ShouldHaveEdge_WhenCreating_GivenOptions(LineStyle style, EndStyle tail, ushort depth, string expected)
	{
		var edge = new Edge(style, tail, depth);
		
		var actual = edge.ToString();
		
		Assert.Equal(expected, actual);
	}
}