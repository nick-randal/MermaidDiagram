namespace MermaidDiagrams.Flowchart;

public static class FlowchartExtensions
{
	public static Text Markdown(this string content) => new(content, true);
}