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

	private static void ProcessCharacter(Character c)
	{
		switch (c.NextMove)
		{
			case Character.ENextMove.MoveRight:
				c.SetLocation(c.Location + 1);
				break;

			case Character.ENextMove.MoveLeft:
				c.SetLocation(c.Location - 1);
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