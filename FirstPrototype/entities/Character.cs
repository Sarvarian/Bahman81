using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype.entities;

public partial class Character : Node2D
{
    [Export] private Sprite2D? sprite2D_;

    public override void _Ready()
    {
        this.AssertFiledSet(nameof(sprite2D_));
    }

    public void ConnectSignals(InputHandler handler)
    {
        handler.MoveRightSignal -= MoveRight;
        handler.MoveRightSignal += MoveRight;
        handler.MoveLeftSignal -= MoveLeft;
        handler.MoveLeftSignal += MoveLeft;
        handler.AttackSignal -= Attack;
        handler.AttackSignal += Attack;
        // We remove and then add signals just to prevent duplication.
    }

    private void MoveRight()
    {
        GD.Print("Move Right");
    }

    private void MoveLeft()
    {
        GD.Print("Move Left");
    }

    private void Attack()
    {
        GD.Print("Attack");
    }

}