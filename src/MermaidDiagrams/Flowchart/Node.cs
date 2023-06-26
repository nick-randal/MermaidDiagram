using MermaidDiagrams.Contracts;
using MermaidDiagrams.Support;

namespace MermaidDiagrams.Flowchart;

public record Node(Identifier Id, Text Text, Shape Shape) : INode
{
	public static INode CreateInvisible(Identifier id) => new Node(id, Text.Create(" ", false), Shape.Box);

	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		var frame = StyleFrames[Shape];

		textBuilder.Append($"{Id}");
		if (Text.IsEmpty is false)
			textBuilder.Append($"{frame.Begin}{Text}{frame.End}");

		textBuilder.Line();
	}

	private static readonly IDictionary<Shape, TextFrame> StyleFrames =
		EnumExtensions
			.DisplayAttributes[typeof(Shape)]
			.ToDictionary(x => (Shape)x.Key, x =>
			{
				var parts = x.Value.ShortName!.Split(' ');
				return new TextFrame(parts[0], parts[1]);
			});

	public static string NextId(string prefix = "id") => $"{prefix}{Interlocked.Increment(ref _idCounter)}";
	private static long _idCounter = 0;
}