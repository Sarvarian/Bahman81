using System;

namespace Survival.aban.entities;

public class Character : Entity
{
    public Character()
    {
        restAction_ = Rest;
        moveRightAction_ = MoveRight;
        moveLeftAction_ = MoveLeft;
        nextMove_ = restAction_;
    }

    public override void Tick()
    {
        nextMove_();
        nextMove_ = restAction_;
    }

    public void SetToMoveRight()
    {
        nextMove_ = moveRightAction_;
    }

    public void SetToMoveLeft()
    {
        nextMove_ = moveLeftAction_;
    }

    private readonly Action restAction_;
    private readonly Action moveRightAction_;
    private readonly Action moveLeftAction_;
    private Action nextMove_;

    private void Rest()
    {
    }

    private void MoveRight()
    {
        NewLocation(Location + 1);
    }

    private void MoveLeft()
    {
        NewLocation(Location - 1);
    }
}