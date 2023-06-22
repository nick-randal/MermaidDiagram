using System.Drawing;
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

	[Fact]
	public Task ShouldHaveValidFlowchart_WhenUsingExampleC()
	{
		When(UsingExampleC, Rendering);

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
				yes =>
				{
					yes.SetLabel("Should I?")
						.Message("A", "B", "Yes!");
				},
				no =>
				{
					no.SetLabel("Or not")
						.Message("A", "B", "No!");
				}
			)
			.Optional("Sometimes we do this...", o =>
			{
				o.Message("A", "B", "We did it!");
			});
	}

	private void UsingExampleC()
	{
		var sequence = Then.Target;

		var a = sequence.CreateParticipant("A", "Alice", true);
		var b = sequence.CreateParticipant("B", "Bob", true);
		var c = sequence.CreateParticipant("C", "Cory", true);

		sequence
			.Parallel(
				first =>
				{
					first.Message(a, b, "Apples");
				},
				second =>
				{
					second.Message(b, a, "Oranges");
				},
				third =>
				{
					third.Message(a, c, "Pears");
				}
			)
			.Highlight(Color.Azure, h =>
			{
				h.Critical(critical =>
					{
						critical.Message(a, b, "Don't lose your sense of humor");
					},
					option1 =>
					{
						option1.Message(a, c, "Stay positive");
					}
				);
			})
			.Break("This is a break", b =>
			{
				b.Message(c, a, "I'm back!");
			});
	}

	public sealed class Thens
	{
		public SequenceDiagram Target { get; set; }
		public string Diagram { get; set; }
	}
}