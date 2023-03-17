using Survival.aban;

namespace UnitTest;

public class MockIEntityNode : IEntityNode
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