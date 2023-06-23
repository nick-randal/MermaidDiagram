using System.Drawing;
using FluentAssertions;
using GwtUnit.XUnit;

namespace MermaidDiagrams.Tests;

public sealed class RgbTests : XUnitTestBase<RgbTests.Thens>
{
	[Fact]
	public void ShouldHaveValidValue_WhenCreating()
	{
		When(Creating);

		Then.Target.Should().BeEquivalentTo(new
		{
			R = 0,
			G = 0,
			B = 0,
			A = 0m
		});
		Then.Target.ToString().Should().Be("rgb(0, 0, 0)");
	}
	
	[Fact]
	public void ShouldHaveValidValue_WhenCreating_GivenColor()
	{
		Given.Color = Color.Red;
		
		When(Creating);

		Then.Target.Should().BeEquivalentTo(new
		{
			R = 255,
			G = 0,
			B = 0,
			A = 1m
		});
		Then.Target.ToString().Should().Be("rgb(255, 0, 0)");
	}
	
	[Fact]
	public void ShouldHaveValidValue_WhenCreating_GivenRgb()
	{
		Then.Target = new Rgb(86, 75, 30, .9m);
		
		When(NotCreating);

		Then.Target.Should().BeEquivalentTo(new
		{
			R = 86,
			G = 75,
			B = 30,
			A = 0.9m
		});
		Then.Target.ToString().Should().Be("rgba(86, 75, 30, 0.9)");
	}
	
	protected override void Creating()
	{
		if(TryGiven<Color>("Color", out var color))
			Then.Target = Rgb.FromColor(color);
		else
			Then.Target = new Rgb();
	}

	public sealed class Thens
	{
		public Rgb Target { get; set; }
	}
}