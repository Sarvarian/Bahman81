using Survival.aban;
using Survival.aban.entities;

namespace UnitTest;

public class TestTheScalar : ClassTestDummyEntity
{
    [Fact]
    public void HasEntityList()
    {
        Assert.IsType<List<Entity>>(scalar_.Entities);
    }

    [Fact]
    public void EntityListIsNotNull()
    {
        Assert.NotNull(scalar_.Entities);
    }

    [Fact]
    public void HasTick()
    {
        AddDummyToWorld();

        var reach = Rng.Next(5, 10);
        for (var i = 1; i < reach; i++)
        {
            scalar_.Tick();
            Assert.Equal(i, TickCallCounter);
        }
    }

    [Fact]
    public void EntitiesInLocation()
    {
        AddDummyToWorld();
        var expected = new[] { Dummy };
        Assert.Equal(expected, scalar_.EntitiesAt(0));
    }


    private readonly TheScalar scalar_ = new();

    private void AddDummyToWorld()
    {
        scalar_.Entities.Add(Dummy);
    }

}