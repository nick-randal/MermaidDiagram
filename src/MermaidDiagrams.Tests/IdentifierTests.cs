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
	
	protected override void Creating()
	{
		Then.Target = new Identifier("");
	}

	public sealed class Thens
	{
		public Identifier Target { get; set; }
	}
}