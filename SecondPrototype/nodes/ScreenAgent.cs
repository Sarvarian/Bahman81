using Godot;
using System;

namespace SecondPrototype.nodes;

public partial class ScreenAgent : Node2D
{
    private static readonly StringName NodeName = "ScreenAgent";

    public static void Setup(Node parent)
    {
        var agent = new ScreenAgent();
        parent.AddChild(agent);
        agent.Name = NodeName;
    }

    public override void _Ready()
    {
        base._Ready();
        GetRootViewport().SizeChanged += ResetScreen;
        ResetScreen();
    }

    public event Action? ScreenUpdatedSignal;

    private aban.Screen screen_;

    private void ResetScreen()
    {
        var newScreen = CreateNewScreen();
        if (screen_ != newScreen)
        {
            screen_ = newScreen;
            ScreenUpdatedSignal?.Invoke();
            GD.Print(screen_.Size);
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
        return (Vector2I)GetRootViewport().GetVisibleRect().Size;
    }
}
