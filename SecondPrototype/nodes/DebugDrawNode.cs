﻿using Godot;

namespace SecondPrototype.nodes;

public partial class DebugDrawNode : Node2D
{
    public static DebugDrawNode Instantiate(Node parent, aban.Grid2D grid, CameraNode camera)
    {
        var node = new DebugDrawNode(grid, camera);
        node.ConnectSignals(camera);
        node.Name = nameof(DebugDrawNode);
        parent.AddChild(node);
        return node;
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
    private readonly Camera2D camera_;
    private Vector2 cameraScreenCenterPos_;

    private void ConnectSignals(CameraNode camera)
    {
        camera.PositionChangedSignal -= QueueRedraw;
        camera.PositionChangedSignal += QueueRedraw;
        // camera.ZoomChangedSignal -= QueueRedraw;
        // camera.ZoomChangedSignal += QueueRedraw;
        // We remove and then add signals just to prevent duplication.
    }

    private DebugDrawNode(aban.Grid2D grid, Camera2D camera)
    {
        grid_ = grid;
        camera_ = camera;
    }

    private void DrawScalar()
    {
        DrawGroundLine();
        DrawScalarNumberLines();
    }

    private void DrawGroundLine()
    {
        var viewportWidth = GetViewportRect().Size.X;
        var start = camera_.GetScreenCenterPosition().X - (viewportWidth / 2);
        var end = start + viewportWidth;
        DrawLine(start, end);
    }

    private void DrawScalarNumberLines()
    {
        DrawScalarSingleNumberLine(0);
        var maxStep = GetViewportRect().Size.X / grid_.CellSize.X / 2;
        for (var i = 1; i < maxStep; i++)
        {
            DrawScalarSingleNumberLine(i * 1);
            DrawScalarSingleNumberLine(i * -1);
        }
    }

    private void DrawScalarSingleNumberLine(int location)
    {
        var start = grid_.LocationToPosition(location);
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

    private void DrawLine(float start, float end)
    {
        DrawLine(
            new Vector2(start, 0),
            new Vector2(end, 0)
        );
    }

}