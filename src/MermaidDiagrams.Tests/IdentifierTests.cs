using FluentAssertions;
using GwtUnit.XUnit;

namespace MermaidDiagrams.Tests;

public sealed class IdentifierTests : XUnitTestBase<IdentifierTests.Thens>
{
	[Fact]
	public void ShouldHaveValidInstance_WhenCreating()
	{
		When(Creating);
		
		Then.Target.Should().NotBeNull().And.BeEquivalentTo(new
		{
			Value = "",
			NoId = true
		});
	}

	[Fact]
	public void ShouldHaveValidInstance_WhenCreating_GivenId()
	{
		Given.Value = "Bob";

		When(Creating);

		Then.Target.Should().NotBeNull().And.BeEquivalentTo(new
		{
			Value = "Bob",
			NoId = false
		});
	}

	protected override void Creating()
	{
		var value = GivenOrDefault("Value", string.Empty);
		Then.Target = new Identifier(value ?? string.Empty);
	}

	public sealed class Thens
	{
		public Identifier Target { get; set; }
	}
}