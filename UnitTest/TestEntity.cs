using Survival.aban.entities;

namespace UnitTest;

public class TestEntity : ClassTestBase
{
	[Fact]
	public void NotNull()
	{
		Assert.NotNull(mock);
	}

	[Fact]
	public void InitializeLocation()
	{
		var location = Rng.Next(1, 10);
		Entity newEntity = new MockEntity(location);
		Assert.Equal(location, newEntity.Location);
	}

	[Fact]
	public void LocationChangedSignal()
	{
		var newLocation = Rng.Next(5, 10);
		var counter = 0;
		mock.LocationChangedSignal += () =>
		{
			Assert.Equal(newLocation, mock.Location);
			counter += 1;
		};

		// Entity has initial location of 0.
		Assert.Equal(0, mock.Location);

		// We give it the same location of 0 and nothing
		// will change. Signal should not called and
		// counter will now increase.
		((MockEntity)mock).NewLocation(0);
		Assert.Equal(0, counter);
		Assert.Equal(0, mock.Location);

		// We give it a new location and signal should
		// called called and make counter increase and
		// entity should have the new location.
		((MockEntity)mock).NewLocation(newLocation);
		Assert.Equal(1, counter);
		Assert.Equal(newLocation, mock.Location);
	}

	private readonly Entity mock = new MockEntity();

}