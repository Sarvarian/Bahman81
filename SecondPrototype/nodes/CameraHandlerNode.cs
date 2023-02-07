using Godot;

namespace SecondPrototype.nodes;

public partial class CameraHandlerNode : Node2D
{
    private static readonly StringName ScenePath = "res://scenes/camera.tscn";
    private static readonly PackedScene Scene = GD.Load<PackedScene>(ScenePath);

    public static CameraHandlerNode Instantiate(
        Node parent,
        aban.Grid2D grid,
        InputHandlerNode input
    )
    {
        var node = new CameraHandlerNode(grid);
        node.ConnectSignals(input);
        node.Name = nameof(CameraHandlerNode);
        parent.AddChild(node);
        return node;
    }

    public readonly CameraNode Camera;

    private aban.Grid2D grid_;

    private CameraHandlerNode(aban.Grid2D grid)
    {
        grid_ = grid;
        Camera = Scene.Instantiate<CameraNode>();
        AddChild(Camera);
    }

    private void ConnectSignals(InputHandlerNode inputHandler)
    {
        inputHandler.GrabCameraSignal -= GrabCamera;
        inputHandler.GrabCameraSignal += GrabCamera;
        inputHandler.DropCameraSignal -= DropCamera;
        inputHandler.DropCameraSignal += DropCamera;
        inputHandler.MouseMovedSignal -= OnMouseMotion;
        inputHandler.MouseMovedSignal += OnMouseMotion;
        inputHandler.CameraZoomInSignal -= ZoomIn;
        inputHandler.CameraZoomInSignal += ZoomIn;
        inputHandler.CameraZoomOutSignal -= ZoomOut;
        inputHandler.CameraZoomOutSignal += ZoomOut;
        // We remove and then add signals just to prevent duplication.
    }

    private const double ZoomDuration = 0.2d;
    private static readonly Vector2 ZoomStep = new Vector2(0.1f, 0.1f);
    private Vector2 targetZoom_ = Vector2.One;
    private bool onPan_ = false;

    private void GrabCamera()
    {
        onPan_ = true;
    }

    private void DropCamera()
    {
        onPan_ = false;
    }

    private void OnMouseMotion(InputEventMouseMotion mouse)
    {
        if (onPan_)
        {
            Position -= mouse.Relative;
        }
    }

    private void ZoomIn()
    {
        targetZoom_ += ZoomStep;
        ActZoom();
    }

    private void ZoomOut()
    {
        targetZoom_ -= ZoomStep;
        ActZoom();
    }

    private void ActZoom()
    {
        var tween = GetTree().CreateTween();
        tween.TweenProperty(Camera, "zoom", targetZoom_, ZoomDuration);
    }

}