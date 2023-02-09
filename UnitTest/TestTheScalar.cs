using Survival.aban;
using Survival.aban.entities;

namespace UnitTest;

public class TestTheScalar : ClassTestBase
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
            character_.SetToMoveRight();
            scalar_.Tick();
            Assert.Equal(i, character_.Location);
        }
    }

    [Fact]
    public void EntitiesInLocation()
    {
        AddDummyToWorld();
        var expected = new Entity[] { character_ };
        Assert.Equal(expected, scalar_.EntitiesAt(0));
    }

    private readonly TheScalar scalar_ = new();
    private readonly Character character_ = new();

    private void AddDummyToWorld()
    {
        scalar_.Entities.Add(character_);
    }

}