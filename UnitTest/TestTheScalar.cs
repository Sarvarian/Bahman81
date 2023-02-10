using Survival.aban;
using Survival.aban.entities;

namespace UnitTest;

public class TestTheScalar : ClassTestBase
{
    [Fact]
    public void HasEntityList()
    {
        Assert.IsType<List<Entity>>(scalar_.Entities);
    }

    [Fact]
    public void EntityListIsNotNull()
    {
        Assert.NotNull(scalar_.Entities);
    }

    [Fact]
    public void HasTick()
    {
        AddCharacterToScalar();

        var reach = Rng.Next(5, 10);
        for (var i = 1; i < reach; i++)
        {
            character_.SetToMoveRight();
            scalar_.Tick();
            Assert.Equal(i, character_.Location);
        }
    }

    [Fact]
    public void EntitiesInLocation()
    {
        AddCharacterToScalar();
        var expected = new Entity[] { character_ };
        Assert.Equal(expected, scalar_.EntitiesAt(0));
    }


    [Fact]
    public void SetToMoveRight()
    {
        AddCharacterToScalar();

        // On initial location is 0.
        Assert.Equal(0, character_.Location);

        // Calling Tick will not change location.
        scalar_.Tick();
        Assert.Equal(0, character_.Location);

        // Calling MoveRight also will not change location.
        character_.SetToMoveRight();
        Assert.Equal(0, character_.Location);

        // But calling Tick after MoveRight will change location.
        scalar_.Tick();
        Assert.Equal(1, character_.Location);

        // Again calling Tick after tick will not change location.
        scalar_.Tick();
        Assert.Equal(1, character_.Location);
    }

    [Fact]
    public void SetToMoveLeft()
    {
        AddCharacterToScalar();

        Assert.Equal(0, character_.Location);

        scalar_.Tick();
        Assert.Equal(0, character_.Location);

        character_.SetToMoveLeft();
        Assert.Equal(0, character_.Location);

        scalar_.Tick();
        Assert.Equal(-1, character_.Location);

        scalar_.Tick();
        Assert.Equal(-1, character_.Location);
    }

    [Fact]
    public void ActionTrigger()
    {
        AddRightSwitchToScalar();
        rightSwitch_.ActionTrigger = MockActionTrigger;
        Assert.Equal(0, actionTriggerCounter_);
        rightSwitch_.DoSwitch();
        Assert.Equal(0, actionTriggerCounter_);
        scalar_.Tick();
        Assert.Equal(1, actionTriggerCounter_);
        scalar_.Tick();
        Assert.Equal(1, actionTriggerCounter_);
    }

    [Fact]
    public void CharacterDoesntMoveRightMoreThenOnePerTick()
    {
        AddCharacterToScalar();
        Assert.Equal(0, character_.Location);
        var random = Rng.Next(5, 10);
        for (var i = 0; i < random; i++)
        {
            character_.SetToMoveRight();
        }
        scalar_.Tick();
        Assert.Equal(1, character_.Location);
    }

    [Fact]
    public void CharacterDoesntMoveLeftMoreThenOnePerTick()
    {
        AddCharacterToScalar();
        Assert.Equal(0, character_.Location);
        var random = Rng.Next(5, 10);
        for (var i = 0; i < random; i++)
        {
            character_.SetToMoveLeft();
        }
        scalar_.Tick();
        Assert.Equal(-1, character_.Location);
    }

    [Fact]
    public void CharacterRightCollision()
    {
        AddCharacterToScalar();
        AddBlockToScalar();
        var location = block_.Location - 1;
        character_.SetLocation(location);
        Assert.Equal(location, character_.Location);
        character_.SetToMoveRight();
        scalar_.Tick();
        Assert.Equal(location, character_.Location);
    }

    [Fact]
    public void CharacterLeftCollision()
    {
        AddCharacterToScalar();
        AddBlockToScalar();
        var location = block_.Location + 1;
        character_.SetLocation(location);
        Assert.Equal(location, character_.Location);
        character_.SetToMoveLeft();
        scalar_.Tick();
        Assert.Equal(location, character_.Location);
    }

    [Fact]
    public void CharacterCanMoveRightOnSwitch()
    {
        AddCharacterToScalar();
        AddRightSwitchToScalar();
        var location = leftSwitch_.Location - 1;
        character_.SetLocation(location);
        Assert.Equal(location, character_.Location);
        character_.SetToMoveRight();
        scalar_.Tick();
        Assert.Equal(location + 1, character_.Location);
    }

    [Fact]
    public void CharacterCanMoveLeftOnSwitch()
    {
        AddCharacterToScalar();
        AddRightSwitchToScalar();
        var location = leftSwitch_.Location + 1;
        character_.SetLocation(location);
        Assert.Equal(location, character_.Location);
        character_.SetToMoveLeft();
        scalar_.Tick();
        Assert.Equal(location - 1, character_.Location);
    }

    [Fact]
    public void FindLeftTargetForSwitch()
    {
        AddBlockToScalar();
        AddRightSwitchToScalar();
        var result = scalar_.FindLeftTargetFor(rightSwitch_);
        Assert.Equal(block_, result);
    }

    [Fact]
    public void FindLeftTargetForSwitchIterationNumberTest()
    {
        AddBlockToScalar();
        var iterTime = Rng.Next(20, 100);
        var mySwitch1 = new Switch(block_.Location + iterTime + 1);
        Assert.Null(scalar_.FindLeftTargetFor(mySwitch1, iterTime));
        var mySwitch2 = new Switch(block_.Location + iterTime);
        Assert.NotNull(scalar_.FindLeftTargetFor(mySwitch2, iterTime));
    }

    [Fact]
    public void FindRightTargetForSwitch()
    {
        AddBlockToScalar();
        AddLeftSwitchToScalar();
        var result = scalar_.FindRightTargetFor(leftSwitch_);
        Assert.Equal(block_, result);
    }

    [Fact]
    public void FindRightTargetForSwitchIterationNumberTest()
    {
        AddBlockToScalar();
        var iterTime = Rng.Next(20, 100);
        var mySwitch1 = new Switch(block_.Location - iterTime - 1);
        Assert.Null(scalar_.FindRightTargetFor(mySwitch1, iterTime));
        var mySwitch2 = new Switch(block_.Location - iterTime);
        Assert.NotNull(scalar_.FindRightTargetFor(mySwitch2, iterTime));
    }

    private readonly TheScalar scalar_ = new();
    private readonly Character character_ = new();
    private readonly Block block_ = new(Rng.Next(11, 20));
    private readonly Switch rightSwitch_ = new(Rng.Next(21, 30));
    private readonly Switch leftSwitch_ = new(Rng.Next(5, 10));
    private int actionTriggerCounter_;

    private void MockActionTrigger()
    {
        actionTriggerCounter_ += 1;
    }

    private void AddCharacterToScalar()
    {
        scalar_.Entities.Add(character_);
    }

    private void AddBlockToScalar()
    {
        scalar_.Entities.Add(block_);
    }

    private void AddRightSwitchToScalar()
    {
        scalar_.Entities.Add(rightSwitch_);
    }

    private void AddLeftSwitchToScalar()
    {
        scalar_.Entities.Add(leftSwitch_);
    }

}