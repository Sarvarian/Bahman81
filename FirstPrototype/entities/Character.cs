using FirstPrototype.extensions;
using Godot;

namespace FirstPrototype.entities;

public partial class Character : Node2D
{
    [Export] private Sprite2D? sprite2D_;
    public bool IsGotInputEvent;

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

    public void SetCoreCharacter(CoreGame.Character character)
    {
        coreCharacter_ = character;
    }

    private CoreGame.Character? coreCharacter_;

    private void MoveRight()
    {
        coreCharacter_!.SetToMoveRight();
        IsGotInputEvent = true;
    }

    private void MoveLeft()
    {
        coreCharacter_!.SetToMoveLeft();
        IsGotInputEvent = true;
    }

    private void Attack()
    {
        // IsGotInputEvent = true;
        // TODO: To Be Implemented.
    }

}