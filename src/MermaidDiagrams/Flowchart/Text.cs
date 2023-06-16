namespace MermaidDiagrams.Flowchart;

public record struct Text
{
	public Text(string Content, bool Markdown = false)
	{
		IsEmpty = string.IsNullOrWhiteSpace(Content);
		this.Content = IsEmpty ? string.Empty : Content.Trim();
		this.Markdown = Markdown;
	}

	public void Deconstruct(out string Content, out bool Markdown)
	{
		Content = this.Content;
		Markdown = this.Markdown;
	}

	public string Content { get; }

	public bool Markdown { get; }
	
	public bool IsEmpty { get; }

	public override string ToString()
	{
		if (IsEmpty)
			return string.Empty;
		
		// todo escape other chars
		var escapedContent =
			Markdown
				? Content.IndexOfAny(MarkdownReservedChars) >= 0
					? $"\"`{Content.Replace("`", "\\`").Replace("\"", QuoteEncoded)}`\""
					: Content
				: Content.IndexOfAny(ReservedChars) >= 0
					? $"\"{Content.Replace("\"", QuoteEncoded)}\""
					: Content;

		return escapedContent;
	}

	public static implicit operator Text(string? content) => new(content);

	public static readonly char[] ReservedChars = "()[]{};\"~|@".ToCharArray();

	public static readonly char[] MarkdownReservedChars = "`\"".ToCharArray();

	public static Text Empty => new(string.Empty);
	
	public const string
		QuoteEncoded = "#quot;";
}