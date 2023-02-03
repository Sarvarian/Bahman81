using CoreGame;

namespace CoreGameTest;

public class TestEntity
{
    [Fact]
    public void NotNull()
    {
        Assert.NotNull(entity_);
    }

    private readonly Entity entity_ = new DummyEntity();
}