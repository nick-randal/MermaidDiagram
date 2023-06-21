namespace MermaidDiagrams.Sequence;

public class SequenceDiagram : MermaidBase
{
	public SequenceDiagram() : base(new BasicDiagramType("sequenceDiagram"))
	{
	}
	
	public SequenceDiagram SetAutoNumbering()
	{
		TryAdd(new AutoNumber());
		
		return this;
	}
	
	public Participant Participant(Identifier id, bool useActor = false) => this.Add(new Participant(id, useActor));
	
	public Participant Participant(Identifier id, string alias, bool useActor = false) => this.Add(new Participant(id, alias, useActor));

	public IParticipant this[Identifier id] => GetRenderableOrThrow<IParticipant>(id);
	
	public virtual string Render()
	{
		var state = new RenderState();
		var builder = new TextBuilder();

		base.Render(builder, state);

		return builder.Text;
	}
}