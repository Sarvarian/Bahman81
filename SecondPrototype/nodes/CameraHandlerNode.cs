using Godot;
using SecondPrototype.extensions;

namespace SecondPrototype.nodes;

public partial class CameraHandlerNode : Node2D
{
    private static readonly StringName ScenePath = "res://scenes/camera_handler.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static CameraHandlerNode Instantiate(Node parent)
    {
        var node = Scene.Instantiate<CameraHandlerNode>();
        parent.AddChild(node);
        return node;
    }

    [Export] private Camera2D? camera2D_;

    public override void _Ready()
    {
        base._Ready();
        this.AssertFiledSet(nameof(camera2D_));
    }
}