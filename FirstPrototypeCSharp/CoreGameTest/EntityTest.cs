using CoreGame;

namespace CoreGameTest;

public class EntityTest
{
    [Fact]
    public void NotNull()
    {
        Assert.NotNull(entity_);
    }

    private readonly Entity entity_ = new EntityDummy();
}