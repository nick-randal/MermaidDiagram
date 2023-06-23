using System.Drawing;
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

	public static implicit operator Rgb(uint rgba) => new((byte)(rgba >> 24), (byte)(rgba >> 16), (byte)(rgba >> 8), ByteToAlpha((byte)rgba));

	public static implicit operator Rgb(string value) => TryParse(value, out var rgb) ? rgb : Black;

	public static Rgb FromColor(Color color) => new(color.R, color.G, color.B, ByteToAlpha(color.A));

	public static bool TryParse(string value, out Rgb rgb)
	{
		rgb = Black;

		if (string.IsNullOrWhiteSpace(value))
			return false;

		var span = value.Trim().AsSpan();
		if (span[0] == '#')
		{
			return ParseHex(ref rgb, span);
		}
		
		if (span[0] is 'r' or 'R' && span[1] is 'g' or 'G' && span[2] is 'b' or 'B')
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

	private static bool ParseHex(ref Rgb rgb, ReadOnlySpan<char> span)
	{
		if (span.Length is not 4 and not 7 and not 9)
			return false;

		var c = new byte[8];

		for (var n = 1; n < span.Length; n++)
		{
			var x = span[n] - 48;
			switch (x)
			{
				case < 0:
					return false;
				case < 10:
					c[n - 1] = (byte)x;
					continue;
			}

			x -= 17;
			switch (x)
			{
				case < 0:
					return false;
				case < 6:
					c[n - 1] = (byte)(x + 10);
					continue;
			}

			x -= 32;
			switch (x)
			{
				case < 0:
					return false;
				case < 6:
					c[n - 1] = (byte)(x + 10);
					break;
				default:
					return false;
			}
		}

		if (span.Length == 4)
		{
			rgb = new Rgb(
				(byte)(c[0] * 16 + 15),
				(byte)(c[1] * 16 + 15),
				(byte)(c[2] * 16 + 15)
			);
			return true;
		}

		rgb = new Rgb(
			(byte)(c[0] * 16 + c[1]),
			(byte)(c[2] * 16 + c[3]),
			(byte)(c[4] * 16 + c[5]),
			span.Length == 7 ? 1m : ByteToAlpha((byte)(c[6] * 16 + c[7]))
		);
		return true;
	}

	private static decimal ByteToAlpha(byte alpha) => Math.Round(alpha / 255m, 2);

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
		
		return Rgb.Black;
	}

	public override void Write(Utf8JsonWriter writer, Rgb value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString());
	}
}