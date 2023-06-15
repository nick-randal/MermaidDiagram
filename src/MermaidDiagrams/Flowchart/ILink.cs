using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public interface ILink : IStatement
{
	public INode From { get; }

	public INode To { get; }

	public Text Label { get; }
	
	public Edge Edge { get; }
}

/*
 Normal	---	----	-----
Normal with arrow	-->	--->	---->
Thick	===	====	=====
Thick with arrow	==>	===>	====>
Dotted	-.-	-..-	-...-
Dotted with arrow	-.->	-..->	-...->
*/