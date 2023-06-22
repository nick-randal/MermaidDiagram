using System.Drawing;

namespace MermaidDiagrams;

public readonly record struct Rgb(byte R, byte G, byte B, decimal A = 1m)
{
	public override string ToString()
	{
		return A is > 0m and < 1m 
			? $"rgba({R}, {G}, {B}, {A})"
			: $"rgb({R}, {G}, {B})";;
	}
	
	public static implicit operator Rgb(Color color) => FromColor(color);
	
	public static Rgb FromColor(Color color) => new(color.R, color.G, color.B, color.A / 255m);
}