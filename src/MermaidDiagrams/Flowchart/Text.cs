namespace MermaidDiagrams.Flowchart;

public record Text
{
	public Text() : this(string.Empty, true, false)
	{
	}

	public Text(string? content, bool markdown = false)
	: this(content?.Trim() ?? string.Empty, string.IsNullOrWhiteSpace(content), markdown)
	{
	}

	internal Text(string content, bool isEmpty, bool markdown)
	{
		Content = content;
		IsEmpty = isEmpty;
		Markdown = markdown;
	}

	public void Deconstruct(out string content, out bool markdown, out bool isEmpty)
	{
		content = Content;
		markdown = Markdown;
		isEmpty = IsEmpty;
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
					: $"\"`{Content}`\""
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