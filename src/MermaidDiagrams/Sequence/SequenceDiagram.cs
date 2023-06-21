namespace MermaidDiagrams.Sequence;

public class SequenceDiagram : DiagramBase
{
	public SequenceDiagram()
	{
		SetType(new BasicDiagramType("sequenceDiagram"));
	}
	
	public IParticipant this[Identifier id] => GetRenderableOrThrow<IParticipant>(id);
	
	public virtual string Render()
	{
		var state = new RenderState();
		var builder = new TextBuilder();

		base.Render(builder, state);

		return builder.Text;
	}
}