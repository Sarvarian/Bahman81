using Godot;

namespace SecondPrototype.nodes;

public partial class DebugDrawNode : Node2D
{
    public static DebugDrawNode Instantiate(Node parent)
    {
        var node = new DebugDrawNode();
        node.Name = nameof(DebugDrawNode);
        parent.AddChild(node);
        return node;
    }

    public override void _Draw()
    {
        base._Draw();
    }
}