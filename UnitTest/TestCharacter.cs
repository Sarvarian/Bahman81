using Survival.aban.entities;

namespace UnitTest;

public class TestCharacter : ClassTestBase
{
    [Fact]
    public void CharacterIsAnEntity()
    {
        Assert.IsAssignableFrom<Entity>(character_);
    }

    [Fact]
    public void HasTickFunction()
    {
        character_.SetToMoveRight();
        character_.Tick();
        Assert.Equal(1, character_.Location);
    }

    [Fact]
    public void TickCallsPerCalls()
    {
        var reach = Rng.Next(5, 10);
        for (var i = 1; i < reach; i++)
        {
            character_.SetToMoveRight();
            character_.Tick();
            Assert.Equal(i, character_.Location);
        }
    }

    [Fact]
    public void SetToMoveRight()
    {
        // On initial location is 0.
        Assert.Equal(0, character_.Location);

        // Calling Tick will not change location.
        character_.Tick();
        Assert.Equal(0, character_.Location);

        // Calling MoveRight also will not change location.
        character_.SetToMoveRight();
        Assert.Equal(0, character_.Location);

        // But calling Tick after MoveRight will change location.
        character_.Tick();
        Assert.Equal(1, character_.Location);

        // Again calling Tick after tick will not change location.
        character_.Tick();
        Assert.Equal(1, character_.Location);
    }

    [Fact]
    public void SetToMoveLeft()
    {
        Assert.Equal(0, character_.Location);

        character_.Tick();
        Assert.Equal(0, character_.Location);

        character_.SetToMoveLeft();
        Assert.Equal(0, character_.Location);

        character_.Tick();
        Assert.Equal(-1, character_.Location);

        character_.Tick();
        Assert.Equal(-1, character_.Location);
    }


    private readonly Character character_ = new();
}