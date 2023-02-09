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
                var unused = new Block(0);
            });
    }

    [Fact]
    public void SetRightTrigger()
    {
        Assert.Null(rightSwitch_.ActionTrigger);
        block_.SetRightTrigger(rightSwitch_);
        Assert.NotNull(rightSwitch_.ActionTrigger);
        Assert.Equal(block_, rightSwitch_.ActionTrigger!.Target);
    }

    [Fact]
    public void SetLeftTrigger()
    {
        Assert.Null(leftSwitch_.ActionTrigger);
        block_.SetLeftTrigger(leftSwitch_);
        Assert.NotNull(leftSwitch_.ActionTrigger);
        Assert.Equal(block_, leftSwitch_.ActionTrigger!.Target);
    }

    [Fact]
    public void SetRightTriggerWillThrowExceptionIfSwitchIsNotAtRightSide()
    {
        Assert.Throws<Survival.exceptions.WrongSide>(() =>
            {
                var leftSideRandomLocation = Rng.Next(
                    block_.Location - 10,
                    block_.Location
                );
                block_.SetRightTrigger(new Switch(leftSideRandomLocation));
            }
        );

        Assert.Throws<Survival.exceptions.WrongSide>(() =>
            {
                block_.SetRightTrigger(new Switch(block_.Location));
            }
        );
    }

    [Fact]
    public void SetLeftTriggerWillThrowExceptionIfSwitchIsNotAtLeftSide()
    {
        Assert.Throws<Survival.exceptions.WrongSide>(() =>
            {
                var rightSideRandomLocation = Rng.Next(
                    block_.Location,
                    block_.Location + 10
                );
                block_.SetLeftTrigger(new Switch(rightSideRandomLocation));
            }
        );

        Assert.Throws<Survival.exceptions.WrongSide>(() =>
            {
                block_.SetLeftTrigger(new Switch(block_.Location));
            }
        );
    }

    [Fact]
    public void SettingRightTriggerMultipleTimesIsAnError()
    {
        block_.SetRightTrigger(rightSwitch_);

        var random = Rng.Next(2, 10);
        for (var i = 0; i < random; i++)
        {
            Assert.Throws<Survival.exceptions.MultipleSetTrigger>(() =>
                {
                    block_.SetRightTrigger(rightSwitch_);
                }
            );
        }
    }

    [Fact]
    public void SettingLeftTriggerMultipleTimesIsAnError()
    {
        block_.SetLeftTrigger(leftSwitch_);

        var random = Rng.Next(2, 10);
        for (var i = 0; i < random; i++)
        {
            Assert.Throws<Survival.exceptions.MultipleSetTrigger>(() =>
                {
                    block_.SetLeftTrigger(leftSwitch_);
                }
            );
        }
    }

    private readonly Block block_ = new(Rng.Next(11, 20));
    private readonly Switch rightSwitch_ = new(Rng.Next(21, 30));
    private readonly Switch leftSwitch_ = new(Rng.Next(5, 10));
}