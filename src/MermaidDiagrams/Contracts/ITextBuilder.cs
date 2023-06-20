namespace MermaidDiagrams.Contracts;

public interface ITextBuilder
{
	ITextBuilder Line();
	
	ITextBuilder Line(string content);

	ITextBuilder Lines(params string[] lines);
	
	ITextBuilder Append(string content);
	
	string Text { get; }
}