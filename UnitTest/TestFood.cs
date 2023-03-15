using Survival.aban.entities;

namespace UnitTest;

public class TestFood : ClassTestBase
{
    [Fact]
    public void IsEntity()
    {
        Assert.IsAssignableFrom<Entity>(food_);
    }

    private static readonly int DefaultLocation = Rng.Next(5, 10);
    private readonly Food food_ = new(DefaultLocation);

}