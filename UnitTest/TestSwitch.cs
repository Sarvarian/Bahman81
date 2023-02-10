using Survival.aban.entities;

namespace UnitTest;

public class TestSwitch : ClassTestBase
{
	[Fact]
	public void IsEntity()
	{
		Assert.IsAssignableFrom<Entity>(switch_);
	}

	[Fact]
	public void InitializeLocation()
	{
		Assert.Equal(InitLocation, switch_.Location);
	}

	[Fact]
	public void Location0WillBeThrowException()
	{
		Assert.Throws<Survival.exceptions.Location0>(
			() =>
			{
				var unused = new Switch(0);
			});
	}

	[Fact]
	public void ShouldSwitchInNextTick()
	{
		Assert.False(switch_.ShouldSwitchInNextTick);
		switch_.DoSwitch();
		Assert.True(switch_.ShouldSwitchInNextTick);
	}

	private static readonly int InitLocation = Rng.Next(5, 10);
	private readonly Switch switch_ = new(InitLocation);


}