using CoreGame;

namespace CoreGameTest;

public class TestWorld : ClassTestDummyEntity
{
    [Fact]
    public void HasEntityList()
    {
        Assert.IsType<List<Entity>>(world_.Entities);
    }

    [Fact]
    public void EntityListIsNotNull()
    {
        Assert.NotNull(world_.Entities);
    }

    [Fact]
    public void EntityListIsEmptyOnPostInitialize()
    {
        Assert.Empty(world_.Entities);
    }

    [Fact]
    public void HasTick()
    {
        world_.Entities.Add(Entity);

        var reach = Rng.Next(5, 10);
        for (var i = 1; i < reach; i++)
        {
            world_.Tick();
            Assert.Equal(i, TickCallCounter);
        }
    }

    
    private readonly World world_ = new();
}