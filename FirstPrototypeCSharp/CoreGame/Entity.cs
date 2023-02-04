namespace CoreGame;

public abstract class Entity
{
    public abstract void Tick();
    public int Location { get; protected set; }
}