using System.Text;
using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public class TextBuilder : ITextBuilder
{
	public TextBuilder()
	{
		_text = new StringBuilder(4096);
	}

	public ITextBuilder Line()
	{
		_text.AppendLine();
		return this;
	}
	
	public ITextBuilder Line(string content)
	{
		_text.AppendLine(content);
		return this;
	}

	public ITextBuilder Lines(params string[] lines)
	{
		foreach(var line in lines)
			_text.AppendLine(line);
		return this;
	}

	public ITextBuilder Append(string content)
	{
		_text.Append(content);
		return this;
	}
	
	public string Text => _text.ToString();

	public override string ToString()
	{
		return _text.ToString();
	}

	private readonly StringBuilder _text;
}