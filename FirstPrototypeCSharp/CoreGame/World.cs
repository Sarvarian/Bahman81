namespace CoreGame;

public class World
{
    public readonly List<Entity> Entities = new();

    public void Tick()
    {
        Entities.ForEach(e => e.Tick());
    }
}