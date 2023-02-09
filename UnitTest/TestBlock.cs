using Survival.aban.entities;

namespace UnitTest;

public class TestBlock : ClassTestBase
{
    [Fact]
    public void IsEntity()
    {
        Assert.IsAssignableFrom<Entity>(block_);
    }

    [Fact]
    public void IsSwitch()
    {
        Assert.IsAssignableFrom<Switch>(block_);
    }

    [Fact]
    public void InitializeLocation()
    {
        var location = Rng.Next(1, 10);
        var newBlock = new Block(location);
        Assert.Equal(location, newBlock.Location);
    }

    [Fact]
    public void Location0WillBeThrowException()
    {
        Assert.Throws<Survival.exceptions.Location0>(
            () =>
            {
                var block = new Block(0);
            });
    }

    private readonly Block block_ = new(Rng.Next(5, 10));
}