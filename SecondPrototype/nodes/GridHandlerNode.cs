using Godot;

namespace SecondPrototype.nodes;

public partial class GridHandlerNode : Node2D
{

    public static GridHandlerNode Instantiate(Node parent)
    {
        var node = new GridHandlerNode();
        parent.AddChild(node);
        return node;
    }

    private readonly HighlighterNode highlighter_;

    private GridHandlerNode()
    {
        highlighter_ = HighlighterNode.Instantiate(this);
    }

}