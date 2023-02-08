using Godot;

namespace SecondPrototype.nodes.no_use_in_editor;

public partial class GridHandler : Node2D
{

    public static GridHandler Instantiate(Node parent)
    {
        var node = new GridHandler();
        node.Name = nameof(GridHandler);
        parent.AddChild(node);
        return node;
    }

    public readonly HighlighterNode Highlighter;
    public readonly aban.Grid2D Grid;

    private GridHandler()
    {
        Highlighter = HighlighterNode.Instantiate(this);
        Grid = new(CalculateCellSize());
    }

    private Vector2I CalculateCellSize()
    {
        var width = Highlighter.GetSize().X;
        return new Vector2I(width, width);
    }
}