using System.Text.Json.Serialization;
using MermaidDiagrams.Contracts;
using MermaidDiagrams.Support;

namespace MermaidDiagrams.Git;

public record GitInit()
{
	[JsonPropertyName("theme")]
	public string Theme { get; init; } = "default";

	[JsonPropertyName("themeVariables")]
	public ThemeVariables? ThemeVariables { get; init; }
}

public class ThemeVariables
{
	[JsonPropertyName("git0")]
	public string? Git0 { get; set; }

	[JsonPropertyName("git1")]
	public string? Git1 { get; set; }

	[JsonPropertyName("git2")]
	public string? Git2 { get; set; }

	[JsonPropertyName("git3")]
	public string? Git3 { get; set; }

	[JsonPropertyName("git4")]
	public string? Git4 { get; set; }

	[JsonPropertyName("git5")]
	public string? Git5 { get; set; }

	[JsonPropertyName("git6")]
	public string? Git6 { get; set; }

	[JsonPropertyName("git7")]
	public string? Git7 { get; set; }

	[JsonPropertyName("gitBranchLabel0")]
	public string? GitBranchLabel0 { get; set; }

	[JsonPropertyName("gitBranchLabel1")]
	public string? GitBranchLabel1 { get; set; }

	[JsonPropertyName("gitBranchLabel2")]
	public string? GitBranchLabel2 { get; set; }

	[JsonPropertyName("gitBranchLabel3")]
	public string? GitBranchLabel3 { get; set; }

	[JsonPropertyName("gitBranchLabel4")]
	public string? GitBranchLabel4 { get; set; }

	[JsonPropertyName("gitBranchLabel5")]
	public string? GitBranchLabel5 { get; set; }

	[JsonPropertyName("gitBranchLabel6")]
	public string? GitBranchLabel6 { get; set; }

	[JsonPropertyName("gitBranchLabel7")]
	public string? GitBranchLabel7 { get; set; }

	[JsonPropertyName("gitBranchLabel8")]
	public string? GitBranchLabel8 { get; set; }

	[JsonPropertyName("gitBranchLabel9")]
	public string? GitBranchLabel9 { get; set; }

	[JsonPropertyName("commitLabelColor")]
	public string? CommitLabelColor { get; set; }

	[JsonPropertyName("commitLabelBackground")]
	public string? CommitLabelBackground { get; set; }

	[JsonPropertyName("commitLabelFontSize")]
	public string? CommitLabelFontSize { get; set; }

	[JsonPropertyName("tagLabelColor")]
	public string? TagLabelColor { get; set; }
	
	[JsonPropertyName("tagLabelBackground")]
	public string? TagLabelBackground { get; set; }
	
	[JsonPropertyName("tagLabelBorder")]
	public string? TagLabelBorder { get; set; }
	
	[JsonPropertyName("tagLabelFontSize")]
	public string? TagLabelFontSize { get; set; }
}