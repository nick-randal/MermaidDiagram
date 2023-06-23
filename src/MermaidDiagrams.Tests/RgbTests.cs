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
		Then.Target.ToString().Should().Be("#00000000");
		Then.Target.ToString(RgbFormat.Rgb).Should().Be("rgb(0, 0, 0)");
		Then.Target.ToUint().Should().Be(0x00000000);
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
		Then.Target.ToString().Should().Be("#FF0000");
		Then.Target.ToString(RgbFormat.Rgb).Should().Be("rgb(255, 0, 0)");
		Then.Target.ToUint().Should().Be(0xFF0000FF);
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
		Then.Target.ToString().Should().Be("#564B1EE5");
		Then.Target.ToString(RgbFormat.Rgb).Should().Be("rgba(86, 75, 30, 0.9)");
		Then.Target.ToUint().Should().Be(0x564B1EE5);
	}

	[Theory,
	 InlineData(null, 0, 0, 0, "1.0"),
	 InlineData(" \t\r\n", 0, 0, 0, "1.0"),
	 InlineData("bob", 0, 0, 0, "1.0"),
	 InlineData("#zoo", 0, 0, 0, "1.0"),
	 InlineData("#Zoo", 0, 0, 0, "1.0"),
	 InlineData("#.ff", 0, 0, 0, "1.0"),
	 InlineData("#:ff", 0, 0, 0, "1.0"),
	 InlineData("junk(0, 0, 0)", 0, 0, 0, "1.0"),
	 InlineData("#000000", 0, 0, 0, "1.0"),
	 InlineData("rgb(0, 0, 0)", 0, 0, 0, "1.0"),
	 InlineData("rgba(0, 0, 0, 1)", 0, 0, 0, "1.0"),
	 InlineData("#FF000040", 255, 0, 0, "0.25"),
	 InlineData("#564B1EE5", 86, 75, 30, "0.9"),
	 InlineData("rgba(86, 75, 30, 0.9)", 86, 75, 30, "0.9"),
	 InlineData("#1Af", 0x1F, 0xAF, 0xFF, "1.0"),
	 InlineData("#1234", 0, 0, 0, "1.0")
	]
	public void ShouldHaveValidValue_WhenParsing(
		string value, byte r, byte g, byte b,
		string a
	)
	{
		Given.Value = value;

		When(Parsing);

		Then.Target.Should().BeEquivalentTo(new
		{
			R = r,
			G = g,
			B = b,
			A = Convert.ToDecimal(a)
		});
	}
	
	[Theory, 
	InlineData(0x000000FF, 0, 0, 0, "1.0"),
	InlineData(0x00000000, 0, 0, 0, "0.0"),
	InlineData(0x564B1EE5, 86, 75, 30, "0.9")
	]
	public void ShouldHaveValidValue_WhenConverting_GivenUint(
		uint value, byte r, byte g, byte b,
		string a
	)
	{
		Given.Value = value;

		When(ConvertingFromUint);

		Then.Target.Should().BeEquivalentTo(new
		{
			R = r,
			G = g,
			B = b,
			A = Convert.ToDecimal(a)
		});
	}

	protected override void Creating()
	{
		if (TryGiven<Color>("Color", out var color))
			Then.Target = Rgb.FromColor(color);
		else
			Then.Target = new Rgb();
	}


	private void Parsing()
	{
		var value = GivenOrDefault("Value", "#12345678");

		Then.Target = value;
	}
	
	private void ConvertingFromUint()
	{
		var value = GivenOrDefault<uint>("Value", 0x12345678);

		Then.Target = value;
	}

	public sealed class Thens
	{
		public Rgb Target { get; set; }
	}
}