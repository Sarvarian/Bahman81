namespace CoreGame;

public class World
{
    public readonly Character Player = new();
    public readonly List<Entity> Entities = new();

    public World()
    {
        Entities.Add(Player);
    }

    public void Tick()
    {
        Entities.ForEach(e => e.Tick());
    }

    public Entity[] EntitiesAt(int location)
    {
        return Entities.Where(e => e.Location == location).ToArray();
    }

}