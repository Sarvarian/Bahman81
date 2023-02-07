using Godot;
using SecondPrototype.extensions;

namespace SecondPrototype.nodes;

public partial class ScreenAgent : Node2D
{
    private static readonly StringName NodeName = "ScreenAgent";

    public static ScreenAgent Instantiate(Node parent)
    {
        var node = new ScreenAgent();
        parent.AddChild(node);
        node.Name = NodeName;
        return node;
    }

    public aban.Screen Screen => screen_;

    public override void _Ready()
    {
        base._Ready();
        GetRootViewport().SizeChanged += ResetSize;
        ResetSize();
    }

    private readonly aban.Screen screen_ = new(Vector2I.Zero);

    private void ResetSize()
    {
        screen_.NewSize(GetRootViewportSize());
    }

    private Viewport GetRootViewport()
    {
        return GetTree().Root.GetViewport();
    }

    private Vector2I GetRootViewportSize()
    {
        return GetRootViewport().GetVisibleRect().Size.ToVec2I();
    }
}
