using CoreGame;

namespace CoreGameTest;

public class TestWorld : BaseTestClass
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
    
    private readonly World world_ = new();
}