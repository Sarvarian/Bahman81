using Godot;
using System;

namespace FirstPrototype;

public class InputHandler
{
    public event Action? MoveRightSignal;
    public event Action? MoveLeftSignal;
    public event Action? AttackSignal;
    public event Action? ImplantEntitySignal;
    public event Action? RemoveEntitySignal;

    public void NewInput(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed(MoveRight))
        {
            MoveRightSignal?.Invoke();
        }
        else if (inputEvent.IsActionPressed(MoveLeft))
        {
            MoveLeftSignal?.Invoke();
        }
        else if (inputEvent.IsActionPressed(Attack))
        {
            AttackSignal?.Invoke();
        }
        else if (inputEvent.IsActionPressed(ImplantEntity))
        {
            ImplantEntitySignal?.Invoke();
        }
        else if (inputEvent.IsActionPressed(RemoveEntity))
        {
            RemoveEntitySignal?.Invoke();
        }
    }

    public void NewMousePosition(Vector2 globalMousePosition)
    {

    }

    private static readonly StringName MoveRight = "move_right";
    private static readonly StringName MoveLeft = "move_left";
    private static readonly StringName Attack = "attack";
    private static readonly StringName ImplantEntity = "implant_entity";
    private static readonly StringName RemoveEntity = "remove_entity";
}