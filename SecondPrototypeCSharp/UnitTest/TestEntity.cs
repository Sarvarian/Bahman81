using SecondPrototype.aban.entities;

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

}