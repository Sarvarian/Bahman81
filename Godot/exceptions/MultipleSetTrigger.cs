using System;

namespace Survival.exceptions;

public class MultipleSetTrigger : Exception
{
	public MultipleSetTrigger()
		: base("Multiple set trigger is a potential bug, so, don't do it.")
	{
	}
}