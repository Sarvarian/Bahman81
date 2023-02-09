using System;

namespace Survival.exceptions;

public class Location0 : Exception
{
	public Location0()
		: base("Location 0 is only for Character entities.")
	{
	}
}