using Godot;
using System;

namespace FirstPrototype;

public class InputHandler
{
    public event Action? MoveRightSignal;
    public event Action? MoveLeftSignal;
    public event Action? AttackSignal;

    public void NewKeyInput(InputEventKey key)
    {
        if (key.IsActionPressed(moveRight_))
        {
            MoveRightSignal?.Invoke();
        }
        else if (key.IsActionPressed(moveLeft_))
        {
            MoveLeftSignal?.Invoke();
        }
        else if (key.IsActionPressed(attack_))
        {
            AttackSignal?.Invoke();
        }
    }

    public void NewMousePosition(Vector2 globalMousePosition)
    {
        
    }

    private readonly StringName moveRight_ = "move_right";
    private readonly StringName moveLeft_ = "move_left";
    private readonly StringName attack_ = "attack";
}