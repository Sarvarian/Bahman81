namespace CoreGame;

public class Character : Entity
{
    public readonly Health Health = new();
    public readonly Hunger Hunger = new();

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
        Location += 1;
    }

    private void MoveLeft()
    {
        Location -= 1;
    }

}