using System;

namespace Survival.exceptions;

public class UnexpectedType : Exception
{
	public UnexpectedType(string expectedTypeName, string actualTypeName)
		: base($"Expect of type {expectedTypeName}" +
			   $" but got type {actualTypeName}.")
	{
	}

}