using GwtUnit.XUnit;
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

		sequence.SetAutoNumbering()
			.Participant("Alice")
			.Participant("Bob")
			.Note("Bob", "Bob is cool", NotePosition.RightOf)
			.Message("Alice", "Bob", "Can you hear me now?", ArrowType.DottedLineCross, true)
			.Comment("topical reference")
			.Message("Bob", "Alice", "Goodbye", ArrowType.SolidLineArrow, false)
			.Note("Alice", "This is a floating note", NotePosition.Over, "Bob");
	}

	private void UsingExampleB()
	{
		var sequence = Then.Target;

		var b = sequence
			.Participant("A", "Alice", true)
			.CreateParticipant("B", "Bob", true);

		sequence
			.Message("A", "B", "Hello Bob, how are you?")
			.Activate(b)
			.Message("B", "A", "Great!", ArrowType.DottedLineArrow)
			.Deactivate(b)
			.Loop("Tell me when", l =>
			{
				l.Message("A", "B", "When!");
			})
			.Alternate(
				"Should I?",
				yes =>
				{
					yes.Message("A", "B", "Yes!");
				},
				"Or not",
				no =>
				{
					no.Message("A", "B", "No!");
				}
			)
			.Optional("Sometimes we do this...", o =>
			{
				o.Message("A", "B", "We did it!");
			});
	}

	public sealed class Thens
	{
		public SequenceDiagram Target { get; set; }
		public string Diagram { get; set; }
	}
}