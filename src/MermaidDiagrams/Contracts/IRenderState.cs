namespace MermaidDiagrams.Contracts;

public interface IRenderState
{
	int Depth { get; }
	
	OnDisposeAction StepIn();
	
	string Indent { get; }

	TStack State<TStack>() where TStack : Enum;
	
	OnDisposeAction PushState<TStack>(TStack state) where TStack : Enum;
}