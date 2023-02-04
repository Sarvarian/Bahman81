using CoreGame;

namespace CoreGameTest;

public class TestBlock : ClassTestBase
{
    [Fact]
    public void IsEntity()
    {
        Assert.IsAssignableFrom<Entity>(block_);
    }

    private readonly Block block_ = new();
}