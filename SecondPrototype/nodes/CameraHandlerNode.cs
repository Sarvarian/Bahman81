using Godot;

namespace SecondPrototype.nodes;

public partial class CameraHandlerNode : Node2D
{
    private static readonly StringName ScenePath = "res://scenes/camera.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static CameraHandlerNode Instantiate(Node parent, aban.Grid2D grid)
    {
        var node = new CameraHandlerNode(grid);
        node.Name = nameof(CameraHandlerNode);
        parent.AddChild(node);
        return node;
    }

    private Camera2D camera_;
    private aban.Grid2D grid_;

    private CameraHandlerNode(aban.Grid2D grid)
    {
        grid_ = grid;
        camera_ = Scene.Instantiate<Camera2D>();
        AddChild(camera_);
    }
}