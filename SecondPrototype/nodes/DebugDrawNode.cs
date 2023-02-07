using Godot;

namespace SecondPrototype.nodes;

public partial class DebugDrawNode : Node2D
{
    public static DebugDrawNode Instantiate(Node parent, aban.Grid2D grid, CameraNode camera)
    {
        var layer = new CanvasLayer();
        parent.AddChild(layer, true);
        var node = new DebugDrawNode(grid, camera, layer);
        layer.AddChild(node);
        return node;
    }

    public override void _EnterTree()
    {
        base._EnterTree();
        layer_.Offset = GetViewportRect().Size / 2;
    }

    public override void _Draw()
    {
        base._Draw();
        DrawScalar();
    }

    private const float NumberLineLength = 10.0f;
    private const float LineWidth = 2.0f;
    private readonly Color color_ = Colors.DarkRed;
    private readonly aban.Grid2D grid_;
    private readonly CameraNode camera_;
    private readonly CanvasLayer layer_;
    private Vector2 cameraOffset_;
    private Vector2 cameraZoom_;
    private float cameraZoomLength_;

    private DebugDrawNode(aban.Grid2D grid, CameraNode camera, CanvasLayer layer)
    {
        Name = nameof(DebugDrawNode);
        grid_ = grid;
        camera_ = camera;
        layer_ = layer;
        ConnectSignals(camera);
    }

    private void ConnectSignals(CameraNode camera)
    {
        camera.PositionChangedSignal -= OnCameraUpdate;
        camera.PositionChangedSignal += OnCameraUpdate;
        camera.ZoomChangedSignal -= OnCameraUpdate;
        camera.ZoomChangedSignal += OnCameraUpdate;
        // We remove and then add signals just to prevent duplication.
    }

    private void OnCameraUpdate()
    {
        cameraOffset_ = camera_.OffsetFromGlobalWorldCenter();
        cameraZoom_ = camera_.TargetZoom;
        cameraZoomLength_ = cameraZoom_.Length();
        QueueRedraw();
    }

    private void DrawScalar()
    {
        DrawGroundLine();
        DrawNumberLines();
    }

    private void DrawGroundLine()
    {
        var halfViewportWidth = GetViewportRect().Size.X / 2;
        var start = 0 - halfViewportWidth;
        var end = halfViewportWidth;
        DrawLine(
            new Vector2(start, cameraOffset_.Y),
            new Vector2(end, cameraOffset_.Y)
        );
    }

    private void DrawNumberLines()
    {
        DrawSingleNumberLine(0);
        var maxStep = GetViewportRect().Size.X / grid_.CellSize.X / 2;
        for (var i = 1; i < maxStep; i++)
        {
            DrawSingleNumberLine(i * 1);
            DrawSingleNumberLine(i * -1);
        }
    }

    private void DrawSingleNumberLine(int location)
    {
        var start = grid_.LocationToPosition(location);
        start.Y += cameraOffset_.Y;
        var end = start + (Vector2.Down * NumberLineLength);
        DrawLine(start, end);
    }

    private void DrawLine(Vector2 start, Vector2 end)
    {
        DrawLine(
            start,
            end,
            color_,
            LineWidth
        );
    }

}