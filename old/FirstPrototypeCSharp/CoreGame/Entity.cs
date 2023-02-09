namespace CoreGame;

public abstract class Entity
{
    public abstract void Tick();
    public int Location { get; protected set; }

    protected Entity()
    {
    }

    protected Entity(int location)
    {
        Location = location;
    }
}