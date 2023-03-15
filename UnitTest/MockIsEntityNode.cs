using Survival.aban;

namespace UnitTest;

public class MockIsEntityNode : IEntityNode
{
	public bool IsActive()
	{
		throw new NotImplementedException();
	}

	public bool IsIdle()
	{
		throw new NotImplementedException();
	}
}