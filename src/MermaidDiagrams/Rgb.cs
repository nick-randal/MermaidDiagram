using System.Drawing;

namespace MermaidDiagrams;

public readonly record struct Rgb(byte R, byte G, byte B, decimal A = 1m)
{
	public override string ToString() => ToString(RgbFormat.Hex);

	public string ToString(RgbFormat format)
	{
		return format switch
		{
			RgbFormat.Rgb => A is > 0m and < 1m 
				? $"rgba({R}, {G}, {B}, {A})"
				: $"rgb({R}, {G}, {B})",
			_ => $"#{R:X2}{G:X2}{B:X2}{(byte)(A * 255):X2}"
		};
	}
	
	public static implicit operator Rgb(Color color) => FromColor(color);
	
	public static Rgb FromColor(Color color) => new(color.R, color.G, color.B, color.A / 255m);
}

public enum RgbFormat
{
	Hex = 0,
	Rgb = 1
}