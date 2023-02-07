using Godot;
using System;

namespace SecondPrototype.nodes;

public partial class CameraNode : Camera2D
{

    public event Action? PositionChangedSignal;
    public event Action? ZoomChangedSignal;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        CheckPosition();
        CheckZoom();
    }

    private Vector2 previousPosition_;
    private Vector2 previousZoom_;

    private void CheckPosition()
    {
        var position = GetScreenCenterPosition();
        if (position != previousPosition_)
        {
            PositionChangedSignal?.Invoke();
            previousPosition_ = position;
        }
    }

    private void CheckZoom()
    {
        if (Zoom != previousZoom_)
        {
            ZoomChangedSignal?.Invoke();
            previousZoom_ = Zoom;
        }
    }

}