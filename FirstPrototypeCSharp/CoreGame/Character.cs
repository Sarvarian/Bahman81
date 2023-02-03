namespace CoreGame;

public class Character : Entity
{
    public readonly Health Health = new();
    public readonly Hunger Hunger = new();
    public override void Tick()
    {
        throw new NotImplementedException();
    }
}