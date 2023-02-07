using Godot;

namespace SecondPrototype.nodes;

public partial class DebugDrawNode : Node2D
{
    public static DebugDrawNode Instantiate(Node parent, aban.Grid2D grid, Camera2D camera)
    {
        var node = new DebugDrawNode(grid, camera);
        node.Name = nameof(DebugDrawNode);
        parent.AddChild(node);
        return node;
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        OrderRedrawIfCameraMoved();
    }

    public override void _Draw()
    {
        base._Draw();
        DrawScalar();
    }

    private readonly Color color_ = Colors.DarkRed;
    private readonly aban.Grid2D grid_;
    private readonly Camera2D camera_;
    private Vector2 cameraScreenCenterPos_;

    private DebugDrawNode(aban.Grid2D grid, Camera2D camera)
    {
        grid_ = grid;
        camera_ = camera;
    }

    private void OrderRedrawIfCameraMoved()
    {
        if (camera_.GetScreenCenterPosition() != cameraScreenCenterPos_)
        {
            QueueRedraw();
            cameraScreenCenterPos_ = camera_.GlobalPosition;
        }
    }

    private void DrawScalar()
    {
        DrawGroundLine();
    }

    private void DrawGroundLine()
    {
        var viewportWidth = GetViewportRect().Size.X;
        var start = camera_.GetScreenCenterPosition().X - (viewportWidth / 2);
        var end = start + viewportWidth;
        DrawLine(
            new Vector2(start, 0),
            new Vector2(end, 0),
            color_
        );
    }
}