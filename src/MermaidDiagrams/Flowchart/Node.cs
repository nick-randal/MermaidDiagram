using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public record Node(string Id, Text Text, Shape Shape) : INode
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		var state = renderState.State<FlowRenderState>();
		var frame = StyleFrames[Shape];
		
		textBuilder.Append($"{Id}");
		if(Text.IsEmpty is false && state.HasFlag(FlowRenderState.NodeIdOnly) is false)
			textBuilder.Append($"{frame.Begin}{Text}{frame.End}");
		if(state.HasFlag(FlowRenderState.Inline) is false)
			textBuilder.Line();
	}

	public static INode Create(string id) => new Node(id, string.Empty, Shape.Box);

	public static INode Create(string id, Shape style) => new Node(id, string.Empty, style);
 
	public static INode Create(string id, Text text) => new Node(id, text, Shape.Box);
 
	public static INode Create(string id, Text text, Shape style) => new Node(id, text, style);
	
	private static readonly IDictionary<Shape, TextFrame> StyleFrames =
		EnumExtensions
			.DisplayAttributes[typeof(Shape)]
			.ToDictionary(x => (Shape)x.Key, x =>
			{
				var parts = x.Value.ShortName!.Split(' ');
				return new TextFrame(parts[0], parts[1]);
			});
}