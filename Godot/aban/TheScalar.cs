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

	private void ProcessCharacter(Character c)
	{
		int futureLocation;
		switch (c.NextMove)
		{
			case Character.ENextMove.MoveRight:
				futureLocation = c.Location + 1;
				if (EntitiesAt(futureLocation).Length == 0)
				{
					c.SetLocation(futureLocation);
				}
				break;

			case Character.ENextMove.MoveLeft:
				futureLocation = c.Location - 1;
				if (EntitiesAt(futureLocation).Length == 0)
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

}