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
        AddDummyToWorld();

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

    [Fact]
    public void EntitiesInLocation()
    {
        AddDummyToWorld();
        var expected = new[] { world_.Player, Dummy };
        Assert.Equal(expected, world_.EntitiesAtLocation(0));
    }


    private readonly World world_ = new();

    private void AddDummyToWorld()
    {
        world_.Entities.Add(Dummy);
    }

}