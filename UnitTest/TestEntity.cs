using Survival.aban.entities;

namespace UnitTest;

public class TestEntity : ClassTestBase
{
    [Fact]
    public void NotNull()
    {
        Assert.NotNull(dummy_);
    }

    [Fact]
    public void InitializeLocation()
    {
        var location = Rng.Next(1, 10);
        Entity newEntity = new DummyEntity(location);
        Assert.Equal(location, newEntity.Location);
    }

    [Fact]
    public void LocationChangedSignal()
    {
        var newLocation = Rng.Next(5, 10);
        var counter = 0;
        dummy_.LocationChangedSignal += () =>
        {
            Assert.Equal(newLocation, dummy_.Location);
            counter += 1;
        };

        // Entity has initial location of 0.
        Assert.Equal(0, dummy_.Location);

        // We give it the same location of 0 and nothing
        // will change. Signal should not called and
        // counter will now increase.
        ((DummyEntity)dummy_).NewLocation(0);
        Assert.Equal(0, counter);
        Assert.Equal(0, dummy_.Location);

        // We give it a new location and signal should
        // called called and make counter increase and
        // entity should have the new location.
        ((DummyEntity)dummy_).NewLocation(newLocation);
        Assert.Equal(1, counter);
        Assert.Equal(newLocation, dummy_.Location);
    }

    private readonly Entity dummy_ = new DummyEntity();

}