using System;

namespace Survival.exceptions;

public class WrongSide : Exception
{
	public WrongSide()
		: base("Entity is at wrong location side of another entity.")
	{
	}

}