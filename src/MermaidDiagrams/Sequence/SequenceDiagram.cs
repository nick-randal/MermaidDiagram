namespace MermaidDiagrams.Sequence;

public class SequenceDiagram : MermaidBase
{
	public SequenceDiagram() : base(new BasicDiagramType("sequenceDiagram"))
	{
	}
	
	public new IParticipant this[Identifier id] => GetRenderableOrThrow<IParticipant>(id);
	
	public Participant CreateParticipant(Identifier id, bool useActor = false) => Add(new Participant(id, useActor));
	
	public Participant CreateParticipant(Identifier id, string alias, bool useActor = false) => Add(new Participant(id, alias, useActor));
	
	public virtual string Render()
	{
		var state = new RenderState();
		var builder = new TextBuilder();

		base.Render(builder, state);

		return builder.Text;
	}
}