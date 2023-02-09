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
			if (e is Character c)
			{
				MoveCharacter(c);
			}
		});
	}

	public Entity[] EntitiesAt(int location)
	{
		return Entities.Where(e => e.Location == location).ToArray();
	}

	private static void MoveCharacter(Character c)
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
}