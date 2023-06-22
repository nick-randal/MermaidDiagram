using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Support;

internal static class DiagramType
{
	internal record Basic(string Name) : IDiagramType
	{
		public void Render(ITextBuilder textBuilder, IRenderState renderState)
		{
			textBuilder.Line(Name);
		}
	}
	
	internal record None : IDiagramType
	{
		public void Render(ITextBuilder textBuilder, IRenderState renderState)
		{
		}

		public string Name => string.Empty;
	}
}