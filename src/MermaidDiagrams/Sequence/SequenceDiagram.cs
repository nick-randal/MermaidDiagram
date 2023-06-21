namespace MermaidDiagrams.Sequence;

public class SequenceDiagram : DiagramBase
{
	public SequenceDiagram()
	{
		SetType(new BasicDiagramType("sequenceDiagram"));
	}
	
	public SequenceDiagram SetAutoNumbering()
	{
		if (!Renderables.Any(r => r is AutoNumber))
		{
			AddRenderables(new AutoNumber());
		}

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