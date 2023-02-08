﻿using Godot;

namespace SecondPrototype.nodes.no_use_in_editor;

public partial class GridHandlerNode : Node2D
{

    public static GridHandlerNode Instantiate(Node parent)
    {
        var node = new GridHandlerNode();
        node.Name = nameof(GridHandlerNode);
        parent.AddChild(node);
        return node;
    }

    public readonly HighlighterNode Highlighter;
    public readonly aban.Grid2D Grid;

    private GridHandlerNode()
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