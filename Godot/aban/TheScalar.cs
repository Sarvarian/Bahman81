using Survival.aban.entities;
using System.Collections.Generic;
using System.Linq;

namespace Survival.aban;

public class TheScalar
{
    public readonly List<Entity> Entities = new();

    public void Tick()
    {
        Entities.ForEach(e =>
        {
            switch (e)
            {
                case Character c:
                    ProcessCharacter(c);
                    break;
                case Switch s:
                    ProcessSwitch(s);
                    break;
            }
        });
    }

    public Entity[] EntitiesAt(int location)
    {
        return Entities.Where(e => e.Location == location).ToArray();
    }

    private bool CanCharacterMoveOn(int location)
    {
        var entities = EntitiesAt(location);
        return entities.Length == 0 || entities.All(entity => entity is not (Character or Block));
    }

    private void ProcessCharacter(Character c)
    {
        int futureLocation;
        switch (c.NextMove)
        {
            case Character.ENextMove.MoveRight:
                futureLocation = c.Location + 1;
                if (CanCharacterMoveOn(futureLocation))
                {
                    c.SetLocation(futureLocation);
                }
                break;

            case Character.ENextMove.MoveLeft:
                futureLocation = c.Location - 1;
                if (CanCharacterMoveOn(futureLocation))
                {
                    c.SetLocation(futureLocation);
                }
                break;

            case Character.ENextMove.Rest:
            default:
                break;
        }

        c.NextMove = Character.ENextMove.Rest;
    }

    private static void ProcessSwitch(Switch s)
    {
        if (s.ShouldSwitchInNextTick)
        {
            s.ActionTrigger?.Invoke();
        }

        s.ShouldSwitchInNextTick = false;
    }

    public Block? FindLeftTargetFor(Switch rightSwitch, int iterationTime = 200)
    {
        return FindTargetFor(rightSwitch, iterationTime, -1);
    }

    public Block? FindRightTargetFor(Switch leftSwitch, int iterationTime = 200)
    {
        return FindTargetFor(leftSwitch, iterationTime, 1);
    }

    private Block? FindTargetFor(
        Switch @switch,
        int iterationTime,
        int direction
    )
    {
        var switchLocation = @switch.Location;
        for (var i = 1; i <= iterationTime; i++)
        {
            var targetLocation = switchLocation + (i * direction);
            var entities = EntitiesAt(targetLocation);
            foreach (var entity in entities)
            {
                if (entity is Block block && block.WireLayer == @switch.WireLayer)
                {
                    return block;
                }
            }
        }
        return null;
    }

}