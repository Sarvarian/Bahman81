using Godot;
using System;

namespace SecondPrototype.nodes.no_use_in_editor;

public partial class InputHandler : Node
{
    public static InputHandler Instantiate(Node parent)
    {
        var node = new InputHandler();
        node.Name = nameof(InputHandler);
        parent.AddChild(node);
        return node;
    }

    public event Action<InputEventMouseMotion>? MouseMovedSignal;
    public event Action? MoveRightSignal;
    public event Action? MoveLeftSignal;
    public event Action? AttackSignal;
    public event Action? ImplantEntitySignal;
    public event Action? RemoveEntitySignal;
    public event Action? GrabCameraSignal;
    public event Action? DropCameraSignal;
    public event Action? CameraZoomInSignal;
    public event Action? CameraZoomOutSignal;

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if (@event is InputEventMouseMotion mouseMotion)
        {
            MouseMovedSignal?.Invoke(mouseMotion);
        }
        else if (@event.IsActionPressed(MoveRight))
        {
            MoveRightSignal?.Invoke();
        }
        else if (@event.IsActionPressed(MoveLeft))
        {
            MoveLeftSignal?.Invoke();
        }
        else if (@event.IsActionPressed(Attack))
        {
            AttackSignal?.Invoke();
        }
        else if (@event.IsActionPressed(ImplantEntity))
        {
            ImplantEntitySignal?.Invoke();
        }
        else if (@event.IsActionPressed(RemoveEntity))
        {
            RemoveEntitySignal?.Invoke();
        }
        else if (@event.IsActionPressed(CameraPan))
        {
            GrabCameraSignal?.Invoke();
        }
        else if (@event.IsActionReleased(CameraPan))
        {
            DropCameraSignal?.Invoke();
        }
        else if (@event.IsActionReleased(CameraZoomIn))
        {
            CameraZoomInSignal?.Invoke();
        }
        else if (@event.IsActionReleased(CameraZoomOut))
        {
            CameraZoomOutSignal?.Invoke();
        }
    }

    private static readonly StringName MoveRight = "move_right";
    private static readonly StringName MoveLeft = "move_left";
    private static readonly StringName Attack = "attack";
    private static readonly StringName ImplantEntity = "implant_entity";
    private static readonly StringName RemoveEntity = "remove_entity";
    private static readonly StringName CameraPan = "camera_pan";
    private static readonly StringName CameraZoomIn = "camera_zoom_in";
    private static readonly StringName CameraZoomOut = "camera_zoom_out";

}