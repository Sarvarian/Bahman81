using Survival.aban.entities;

namespace UnitTest;

public class TestSwitch : ClassTestBase
{
    [Fact]
    public void IsEntity()
    {
        Assert.IsAssignableFrom<Entity>(switch_);
    }

    private readonly Switch switch_ = new();

}