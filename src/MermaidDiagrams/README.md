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

- Use `SetOptions` to set theme
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

## Git

### Create a git graph

Declare a graph and render it to a string.

```csharp
var git = new GitGraph();
var output = git.Render();
```

### Examples

```csharp
git
    .Branch("A", 3)
    .Branch("B", 2)
    .Branch("C", 1)
    .Commit()
    .Commit()
    .Checkout("A")
    .Commit();
```

```csharp
git
    .Commit()
    .Commit("bob")
    .Commit("sue", CommitType.Reverse)
    .Commit(tag: "tag")
    .Branch(hotfix)
    .Checkout(hotfix)
    .Commit()
    .Commit(CommitType.Reverse)
    .Merge(git.MainBranch)
    .Branch("POC")
    .CherryPick("sue");
```

Change options

```csharp
git.SetOptions(new GitGraphInit
{
    GitGraph = new()
    {
        ShowBranches = false,
        ShowCommitLabel = false,
        MainBranchName = "master",
        MainBranchOrder = 4,
        RotateCommitLabel = false
    },
    Theme = "base",
    ThemeVariables = new ()
    {
        Git0 = "#FF0000"
    }
});
```

## Sequence diagrams

### Create a sequence diagram

Declare a diagram and render it to a string.

```csharp
var sequence = new SequenceDiagram();
var output = sequence.Render();
```

### Examples

```csharp
sequence
    .SetAutoNumbering()
    .Participant("Alice")
    .Participant("Bob")
    .Note("Bob", "Bob is cool", NotePosition.RightOf)
    .Message("Alice", "Bob", "Can you hear me now?", ArrowType.DottedLineCross, true)
    .Comment("topical reference")
    .Message("Bob", "Alice", "Goodbye", ArrowType.SolidLineArrow, false)
    .Note("Alice", "This is a floating note", NotePosition.Over, "Bob");
```

Loop, alternate and optional

```csharp
var b = sequence
    .Participant("A", "Alice", true)
    .CreateParticipant("B", "Bob", true);

sequence
    .Message("A", "B", "Hello Bob, how are you?")
    .Activate(b)
    .Message("B", "A", "Great!", ArrowType.DottedLineArrow)
    .Deactivate(b)
    .Loop("Tell me when", l =>
    {
        l.Message("A", "B", "When!");
    })
    .Alternate(
        yes =>
        {
            yes.SetLabel("Should I?")
                .Message("A", "B", "Yes!");
        },
        no =>
        {
            no.SetLabel("Or not")
                .Message("A", "B", "No!");
        }
    )
    .Optional("Sometimes we do this...", o =>
    {
        o.Message("A", "B", "We did it!");
    });
```

Parallel, highlight and break

```csharp
sequence
    .Parallel(
        first =>
        {
            first.Message(a, b, "Apples");
        },
        second =>
        {
            second.Message(b, a, "Oranges");
        },
        third =>
        {
            third.Message(a, c, "Pears");
        }
    )
    .Highlight(Color.Azure, h =>
    {
        h.Critical(critical =>
            {
                critical.Message(a, b, "Don't lose your sense of humor");
            },
            option1 =>
            {
                option1.Message(a, c, "Stay positive");
            }
        );
    })
    .Break("This is a break", b =>
    {
        b.Message(c, a, "I'm back!");
    });
```

## Other charts

To be done...