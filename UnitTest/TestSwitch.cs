using Survival.aban.entities;

namespace UnitTest;

public class TestSwitch : ClassTestBase
{
    [Fact]
    public void IsEntity()
    {
        Assert.IsAssignableFrom<Entity>(switch_);
    }

    [Fact]
    public void InitializeLocation()
    {
        Assert.Equal(InitLocation, switch_.Location);
    }

    [Fact]
    public void ActionTrigger()
    {
        switch_.ActionTrigger = MockActionTrigger;
        Assert.Equal(0, actionTriggerCounter_);
        switch_.DoSwitch();
        Assert.Equal(1, actionTriggerCounter_);
    }

    [Fact]
    public void Location0WillBeThrowException()
    {
        Assert.Throws<Survival.exceptions.Location0>(
            () =>
            {
                var unused = new Switch(0);
            });
    }

    private static readonly int InitLocation = Rng.Next(5, 10);
    private readonly Switch switch_ = new(InitLocation);
    private int actionTriggerCounter_;

    private void MockActionTrigger()
    {
        actionTriggerCounter_ += 1;
    }

}