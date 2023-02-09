using Survival.aban.utilities;

namespace UnitTest;

public class TestWhenTrueNeverFalse
{
	[Fact]
	public void InitAsFalse()
	{
		Assert.False(data_);
	}

	[Fact]
	public void CanTurnTrue()
	{
		Assert.False(data_);
		data_.MakeTrue();
		Assert.True(data_);
	}

	private WhenTrueNeverFalse data_;
}