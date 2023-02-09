using Survival.exceptions;
using System;

namespace Survival.aban.entities;

public class Switch : Entity
{
	public Action? ActionTrigger;

	public Switch(int location)
		: base(location)
	{
		if (location == 0)
		{
			throw new Location0();
		}
	}

	public void DoSwitch()
	{
		ActionTrigger?.Invoke();
	}
}