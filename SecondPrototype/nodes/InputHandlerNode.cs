using Godot;
using System;

namespace SecondPrototype.nodes;

public partial class InputHandlerNode : Node
{
    public static InputHandlerNode Instantiate(Node parent)
    {
        var node = new InputHandlerNode();
        node.Name = nameof(InputHandlerNode);
        parent.AddChild(node);
        return node;
    }

    public event Action? MoveRightSignal;
    public event Action? MoveLeftSignal;
    public event Action? AttackSignal;
    public event Action? ImplantEntitySignal;
    public event Action? RemoveEntitySignal;
    public event Action? GrabCameraSignal;
    public event Action? DropCameraSignal;
    public event Action<InputEventMouseMotion>? MouseMovedSignal;

    public override void _UnhandledInput(InputEvent @event)
    {
        base._UnhandledInput(@event);
        if (@event.IsActionPressed(MoveRight))
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
        else if (@event is InputEventMouseMotion mouseMotion)
        {
            MouseMovedSignal?.Invoke(mouseMotion);
        }
    }

    private static readonly StringName MoveRight = "move_right";
    private static readonly StringName MoveLeft = "move_left";
    private static readonly StringName Attack = "attack";
    private static readonly StringName ImplantEntity = "implant_entity";
    private static readonly StringName RemoveEntity = "remove_entity";
    private static readonly StringName CameraPan = "camera_pan";

}