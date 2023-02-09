using Survival.aban.entities;

namespace UnitTest;

public class TestEntity : ClassTestDummyEntity
{
    [Fact]
    public void NotNull()
    {
        Assert.NotNull(Dummy);
    }

    [Fact]
    public void HasTickFunction()
    {
        Dummy.Tick();
        Assert.Equal(1, TickCallCounter);
    }

    [Fact]
    public void TickCallsPerCalls()
    {
        var reach = Rng.Next(5, 10);
        for (var i = 1; i < reach; i++)
        {
            Dummy.Tick();
            Assert.Equal(i, TickCallCounter);
        }
    }

    [Fact]
    public void InitializeLocation()
    {
        var location = Rng.Next(1, 10);
        Entity newEntity = new DummyEntity(DummyTickFunction, location);
        Assert.Equal(location, newEntity.Location);
    }

    [Fact]
    public void LocationChangedSignal()
    {
        var newLocation = Rng.Next(5, 10);
        var counter = 0;
        Dummy.LocationChangedSignal += () =>
        {
            Assert.Equal(newLocation, Dummy.Location);
            counter += 1;
        };

        // Entity has initial location of 0.
        Assert.Equal(0, Dummy.Location);

        // We give it the same location of 0 and nothing
        // will change. Signal should not called and
        // counter will now increase.
        ((DummyEntity)Dummy).NewLocation(0);
        Assert.Equal(0, counter);
        Assert.Equal(0, Dummy.Location);

        // We give it a new location and signal should
        // called called and make counter increase and
        // entity should have the new location.
        ((DummyEntity)Dummy).NewLocation(newLocation);
        Assert.Equal(1, counter);
        Assert.Equal(newLocation, Dummy.Location);
    }


}