using CoreGame;

namespace CoreGameTest;

public class TestBlock : ClassTestBase
{
    [Fact]
    public void IsEntity()
    {
        Assert.IsAssignableFrom<Entity>(block_);
    }

    [Fact]
    public void InitializeLocation()
    {
        var location = Rng.Next(1, 10);
        var newBlock = new Block(location);
        Assert.Equal(location, newBlock.Location);
    }

    private readonly Block block_ = new();
}