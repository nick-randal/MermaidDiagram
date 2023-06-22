namespace MermaidDiagrams.Sequence;

public class SequenceDiagram : SequenceBase
{
	public SequenceDiagram() : base(new BasicDiagramType("sequenceDiagram"))
	{
	}

	public virtual string Render()
	{
		var state = new RenderState();
		var builder = new TextBuilder();

		base.Render(builder, state);

		return builder.Text;
	}
}