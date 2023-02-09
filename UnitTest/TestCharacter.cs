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
    public void SetLocation()
    {
        Assert.Equal(0, character_.Location);
        var newLocation = Rng.Next(5, 10);
        character_.SetLocation(newLocation);
        Assert.Equal(newLocation, character_.Location);
    }


    [Fact]
    public void SetToMoveRight()
    {
        Assert.Equal(
            Character.ENextMove.Rest,
            character_.NextMove
        );
        character_.SetToMoveRight();
        Assert.Equal(
            Character.ENextMove.MoveRight,
            character_.NextMove
        );
    }

    [Fact]
    public void SetToMoveLeft()
    {
        Assert.Equal(
            Character.ENextMove.Rest,
            character_.NextMove
        );
        character_.SetToMoveLeft();
        Assert.Equal(
            Character.ENextMove.MoveLeft,
            character_.NextMove
        );
    }

    private readonly Character character_ = new();
}