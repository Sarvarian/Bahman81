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
    public void EntityListAfterInitOnlyHasOneEntityAndThatIsPlayer()
    {
        Assert.Single(world_.Entities);
        Assert.Equal(world_.Player, world_.Entities[0]);
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

    [Fact]
    public void InstantiatePlayer()
    {
        Assert.Equal(0, world_.Entities[0].Location);
        Assert.Equal(world_.Entities[0], world_.Player);
    }

    private readonly World world_ = new();
}