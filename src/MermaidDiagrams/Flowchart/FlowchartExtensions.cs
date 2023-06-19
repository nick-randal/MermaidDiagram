﻿using MermaidDiagrams.Contracts;

namespace MermaidDiagrams.Flowchart;

public static class FlowchartExtensions
{
	public static T Link<T>(this T chart, IIdentifiable a, IIdentifiable b, Edge? edge = null)
		where T : FlowchartBase
	{
		chart.Add(new Link(a, (b, edge ?? Edge.Open)));
		return chart;
	}

	public static T Link<T>(this T chart, string a, string b, Edge? edge = null)
		where T : FlowchartBase => chart.Link((NodeId)a, (NodeId)b, edge);
	
	public static T Link<T>(this T chart, IIdentifiable a, string b, Edge? edge = null)
		where T : FlowchartBase => chart.Link(a, (NodeId)b, edge);
	
	public static T Link<T>(this T chart, string a, IIdentifiable b, Edge? edge = null)
		where T : FlowchartBase => chart.Link((NodeId)a, b, edge);
	
	public static T Links<T>(this T chart, INode anchor, IEnumerable<(IIdentifiable Node, Edge Edge)> to)
		where T : FlowchartBase => chart.Add(new Link(anchor, to as (IIdentifiable Node, Edge Edge)[] ?? to.ToArray()));
	
	public static T Links<T>(this T chart, IEnumerable<IIdentifiable> nodes, Edge edge)
		where T : FlowchartBase
	{
		var enumerable = nodes as INode[] ?? nodes.ToArray();
		return chart.Add(new Link(enumerable.First(), enumerable.Skip(1).Select(x => (x, edge)).ToArray()));
	}

	public static T Invisible<T>(this T chart, string id)
		where T : FlowchartBase
	{
		var node = chart.AddAnd(Flowchart.Node.Invisible(id));

		var def = chart.GetClassDefinitions().GetOrCreate(ClassDef.InvisibleName, () => ClassDef.Invisible);
		def.Assign(node);

		return chart;
	}

	public static T Node<T>(this T chart)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(null, string.Empty, Shape.Box));

	public static T Node<T>(this T chart, string id)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(id, string.Empty, Shape.Box));

	public static T Node<T>(this T chart, Shape style)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(Flowchart.Node.NextId(), string.Empty, style));

	public static T Node<T>(this T chart, Text text, Shape style)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(Flowchart.Node.NextId(), text, style));

	public static T Node<T>(this T chart, string id, Text text, Shape style)
		where T : FlowchartBase => chart.Add(Flowchart.Node.Create(id, text, style));

	public static Text Markdown(this string content) => new(content, true);
}