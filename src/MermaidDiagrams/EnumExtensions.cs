using System.Reflection;

namespace MermaidDiagrams;

public static class EnumExtensions
{
	public static string GetShortName<T>(this T target)
		where T : Enum
	{
		if (DisplayAttributes.TryGetValue(typeof(T), out var attributes) is false)
			throw new NotSupportedException($"Enum {typeof(T).Name} is not supported.");

		return attributes.TryGetValue(target, out var displayAttribute)
			? displayAttribute.ShortName ?? string.Empty
			: string.Empty;
	}

	internal static readonly IDictionary<Type, IDictionary<Enum, DisplayAttribute>> DisplayAttributes = new Dictionary<Type, IDictionary<Enum, DisplayAttribute>>
	{
		{ typeof(Flowchart.FlowDirection), ResolveDisplayAttributeDictionary(typeof(Flowchart.FlowDirection)) },
		{ typeof(Flowchart.Shape), ResolveDisplayAttributeDictionary(typeof(Flowchart.Shape)) }
	};

	private static IDictionary<Enum, DisplayAttribute> ResolveDisplayAttributeDictionary(Type type)
		=> type
			.GetFields(BindingFlags.Public | BindingFlags.Static)
			.Select(f =>
			{
				var displayAttr = f.GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();
				var value = (Enum)f.GetValue(null)!;
				return new KeyValuePair<Enum, DisplayAttribute>(
					value,
					displayAttr ?? BuildDefaultDisplayAttribute(value)
				);
			})
			.ToDictionary(x => x.Key, x => x.Value);

	private static DisplayAttribute BuildDefaultDisplayAttribute(Enum e)
	{
		const string unknown = "?";

		return new DisplayAttribute
		{
			AutoGenerateField = false,
			AutoGenerateFilter = false,
			Description = unknown + e,
			GroupName = unknown + e,
			Name = unknown + e,
			Order = 0,
			Prompt = unknown + e,
			ResourceType = typeof(object),
			ShortName = unknown + e
		};
	}
}