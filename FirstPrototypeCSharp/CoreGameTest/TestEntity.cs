using CoreGame;

namespace CoreGameTest;

public class TestEntity : ClassTestDummyEntity
{
    [Fact]
    public void NotNull()
    {
        Assert.NotNull(Entity);
    }

    [Fact]
    public void HasTickFunction()
    {
        Entity.Tick();
        Assert.Equal(1, TickCallCounter);
    }

    [Fact]
    public void TickCallsPerCalls()
    {
        var reach = Rng.Next(5, 10);
        for (var i = 1; i < reach; i++)
        {
            Entity.Tick();
            Assert.Equal(i, TickCallCounter);
        }
    }

}