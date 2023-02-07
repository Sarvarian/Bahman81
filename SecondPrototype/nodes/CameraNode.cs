using Godot;
using System;

namespace SecondPrototype.nodes;

public partial class CameraNode : Camera2D
{
    public Vector2 TargetZoom { get; private set; } = Vector2.One;
    public event Action? PositionChangedSignal;
    public event Action? ZoomChangedSignal;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        CheckPosition();
        CheckZoom();
    }

    public void ZoomIn()
    {
        TargetZoom += ZoomStep;
        ActZoom();
    }

    public void ZoomOut()
    {
        TargetZoom -= ZoomStep;
        ActZoom();
    }

    public Vector2 Offset()
    {
        return GetScreenCenterPosition() * -1 * TargetZoom;
    }

    private const double ZoomDuration = 0.2d;
    private static readonly Vector2 MinZoom = new Vector2(0.4f, 0.4f);
    private static readonly Vector2 MaxZoom = new Vector2(2.0f, 2.0f);
    private static readonly Vector2 ZoomStep = new Vector2(0.1f, 0.1f);
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

    private void ActZoom()
    {
        TargetZoom = TargetZoom.Clamp(MinZoom, MaxZoom);
        var tween = GetTree().CreateTween();
        tween.SetProcessMode(Tween.TweenProcessMode.Physics);
        tween.TweenProperty(this, "zoom", TargetZoom, ZoomDuration);
    }

}