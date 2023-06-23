using System.Text.Json.Serialization;

namespace MermaidDiagrams.Git;

public record GitGraphInit()
{
	[JsonPropertyName("theme")]
	public string Theme { get; init; } = "default";

	[JsonPropertyName("themeVariables")]
	public ThemeVariables? ThemeVariables { get; init; }
	
	[JsonPropertyName("gitGraph")]
	public GitGraphOptions GitGraph { get; init; } = new();
	
	public static GitGraphInit CreateCustomTheme() => new ()
	{
		Theme = "base",
		ThemeVariables = new ThemeVariables()
	};
}

public record GitGraphOptions()
{
	[JsonPropertyName("showBranches")]
	public bool ShowBranches { get; init; } = true;
	
	[JsonPropertyName("showCommitLabel")]
	public bool ShowCommitLabel { get; init; } = true;
	
	[JsonPropertyName("mainBranchName")]
	public string MainBranchName { get; init; } = "main";
	
	[JsonPropertyName("mainBranchOrder")]
	public int MainBranchOrder { get; init; } = 0;
	
	[JsonPropertyName("rotateCommitLabel")]
	public bool RotateCommitLabel { get; init; } = true;
}