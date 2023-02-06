using Godot;
using SecondPrototype.aban;

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
        GetTree().Root.SizeChanged += ResetScreen;
        ResetScreen();
    }

    private aban.Screen screen_ = new();

    private void ResetScreen()
    {
        var size = GetTree().Root.Size;
        screen_ = new Screen(size);
    }
}