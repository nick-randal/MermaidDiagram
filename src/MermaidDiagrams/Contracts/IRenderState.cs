using MermaidDiagrams.Support;

namespace MermaidDiagrams.Contracts;

public interface IRenderState
{
	int Depth { get; }
	
	OnDisposeAction StepIn();
	
	string Indent { get; }
}