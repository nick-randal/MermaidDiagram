namespace MermaidDiagrams.Contracts;

public interface IRenderable
{
	void Render(ITextBuilder textBuilder, IRenderState renderState);
}