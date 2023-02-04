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

    public void TickMoveRight()
    {
        Player.SetToMoveRight();
        Tick();
    }

    public void TickMoveLeft()
    {
        Player.SetToMoveLeft();
        Tick();
    }

}