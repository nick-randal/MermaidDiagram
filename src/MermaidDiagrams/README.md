# Mermaid Diagrams

.net support for Mermaid diagrams

## Flowcharts

### Create a flowchart

Declare a chart and render it to a string.

```csharp
var flow = new FlowchartGraph(FlowDirection.TopBottom);
var output = flow.Render();
```

### Setup a flowchart

- Add `init` directive to set theme
- Set a title with `SetHeader`
- Add a comment with `AddComment`
- Add nodes with `Node`
- Add links with `Link`

```csharp
flow.SetOptions(new FlowchartInit { Theme = KnownThemes.Forest });

flow.SetHeader(new Header("This is a test"));

flow.Comment("No comment");

flow.Node("A", "Hard edge", Shape.Box)
	.Node("B", "Round edge", Shape.RoundedBox);

flow.Link(flow["A"], flow["B"], Edge.Arrow.WithLabel("Link text"));

var c = flow.CreateNode("C", "Decision", Shape.Rhombus);

var d = flow.CreateNode("D", "Result One", Shape.Trapezoid);

var e = flow.CreateNode("E", "Result Two", Shape.Circle);

flow.Link(flow["B"], c, Edge.Arrow);
flow.Link(c, d, Edge.Arrow.WithLabel("Yes"));
flow.Link(c, e, Edge.Arrow.WithLabel("No"));
```

### Subgraphs

```csharp
flow.Subgraph("Outer", Identifier.Next("sg"), FlowDirection.TopBottom, 
    sub =>
	{
		sub.Node("Something", Shape.Stadium);

		var sg = sub.Subgraph("Inner", "in1");
		sg.Node("A", "Hard edge", Shape.Box);
	});
```

### Theme and Styles

Define a style by name and assign it to a node by it's identifier.

```csharp
var cd = flow.GetClassDefinitions();

cd.GetOrCreate("neat", s
    => new ClassDef(s)
        .Style("fill", "#f96")
        .Style("stroke", "#333")
        .Style("stroke-width", "2px")
        .Assign("A")
    );
```

Use a stock theme.

```csharp
flow.SetOptions(new FlowchartInit { Theme = KnownThemes.Forest });
```

Defining a custom theme

```csharp
var customTheme = FlowchartInit.CreateCustomTheme(theme =>
    {
        theme.PrimaryColor = "#f96";
        theme.SecondaryColor = "#363";
        theme.LineColor = "#363";
        theme.TertiaryColor = "#f96";
        theme.PrimaryBorderColor = "#333";
        theme.PrimaryTextColor = "#633";
    });
    
flow.SetOptions(customTheme);
```

## Other charts

To be done...