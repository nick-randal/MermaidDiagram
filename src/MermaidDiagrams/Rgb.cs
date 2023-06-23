using System.Drawing;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MermaidDiagrams;

public enum RgbFormat
{
	Hex = 0,
	Rgb = 1
}

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
			_ => A >= 1m ? $"#{R:X2}{G:X2}{B:X2}" : $"#{R:X2}{G:X2}{B:X2}{(byte)(A * 255):X2}"
		};
	}
	
	public uint ToUint() => (uint)((R << 24) | (G << 16) | (B << 8) | (byte)(A * 255));
	
	public static implicit operator Rgb(Color color) => FromColor(color);
	
	public static implicit operator Rgb(uint rgba) => new((byte)(rgba >> 24), (byte)(rgba >> 16), (byte)(rgba >> 8), (byte)rgba);

	public static implicit operator Rgb(string value) => TryParse(value, out var rgb) ? rgb : Black;
	
	public static Rgb FromColor(Color color) => new(color.R, color.G, color.B, color.A / 255m);
	
	public static bool TryParse(string value, out Rgb rgb)
	{
		rgb = Black;
		
		if (string.IsNullOrWhiteSpace(value))
			return false;
		
		value = value.Trim();
		
		if (value.StartsWith("#"))
		{
			if (value.Length is not 3 and not 7 and not 9)
				return false;
			if (value.Length == 7)
				value += "FF";
			if (uint.TryParse(value.Substring(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var rgba))
			{
				rgb = new Rgb((byte)(rgba >> 24), (byte)(rgba >> 16), (byte)(rgba >> 8), (byte)rgba);
				return true;
			}
		}
		else if (value.StartsWith("rgb"))
		{
			var match = Regex.Match(value, @"rgba?\((?<r>\d+),\s*(?<g>\d+),\s*(?<b>\d+)(?:,\s*(?<a>\d+(?:\.\d+)?))?\)");
			if (match.Success)
			{
				rgb = new Rgb(byte.Parse(match.Groups["r"].Value), byte.Parse(match.Groups["g"].Value), byte.Parse(match.Groups["b"].Value), match.Groups["a"].Success ? decimal.Parse(match.Groups["a"].Value) : 1m);
				return true;
			}
		}
		return false;
	}

	public const uint Black = 0x000000ff;
}

public sealed class RgbToStringConverter : JsonConverter<Rgb>
{
	public override Rgb Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.String)
		{
			if (Rgb.TryParse(reader.GetString(), out var rgb))
				return rgb;
		}

		// fallback to default handling
		return Rgb.Black;
	}

	public override void Write(Utf8JsonWriter writer, Rgb value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString());
	}
}