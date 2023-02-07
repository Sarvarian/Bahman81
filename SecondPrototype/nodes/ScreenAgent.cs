using Godot;
using SecondPrototype.extensions;
using System;

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

    public Vector2I Size => screen_.Size;
    public Vector2I Center => screen_.Center;
    public event Action? ScreenUpdatedSignal;

    public override void _Ready()
    {
        base._Ready();
        GetRootViewport().SizeChanged += ResetScreen;
        ResetScreen();
    }

    private aban.Screen screen_;

    private void ResetScreen()
    {
        var newScreen = CreateNewScreen();
        if (screen_ != newScreen)
        {
            screen_ = newScreen;
            ScreenUpdatedSignal?.Invoke();
            GD.Print($"screen_.Size");
        }
    }

    private aban.Screen CreateNewScreen()
    {
        var size = GetRootViewportSize();
        return new aban.Screen(size);
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
