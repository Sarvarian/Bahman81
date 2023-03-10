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
    public void FindRightTargetForSwitch()
    {
        AddBlockToScalar();
        AddLeftSwitchToScalar();
        var result = scalar_.FindRightTargetFor(leftSwitch_);
        Assert.Equal(block_, result);
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
    public void FindRightTargetForSwitchIterationNumberTest()
    {
        AddBlockToScalar();
        var iterTime = Rng.Next(20, 100);
        var mySwitch1 = new Switch(block_.Location - iterTime - 1);
        Assert.Null(scalar_.FindRightTargetFor(mySwitch1, iterTime));
        var mySwitch2 = new Switch(block_.Location - iterTime);
        Assert.NotNull(scalar_.FindRightTargetFor(mySwitch2, iterTime));
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
    public void FindRightTargetForSwitchConsiderDifferentWireLayer()
    {
        AddBlockToScalar();
        AddRightSwitchToScalar();
        var layer = (byte)Rng.Next(1, 5);
        block_.WireLayer = layer;
        leftSwitch_.WireLayer = (byte)(layer + 1);
        Assert.Null(scalar_.FindRightTargetFor(leftSwitch_));
        leftSwitch_.WireLayer = layer;
        Assert.Equal(block_, scalar_.FindRightTargetFor(leftSwitch_));
    }

    [Fact]
    public void FindLeftTargetForSwitchConsiderDifferentWireLayer()
    {
        AddBlockToScalar();
        AddRightSwitchToScalar();
        var layer = (byte)Rng.Next(1, 5);
        block_.WireLayer = layer;
        rightSwitch_.WireLayer = (byte)(layer + 1);
        Assert.Null(scalar_.FindLeftTargetFor(rightSwitch_));
        rightSwitch_.WireLayer = layer;
        Assert.Equal(block_, scalar_.FindLeftTargetFor(rightSwitch_));
    }

    [Fact]
    public void FindRightTargetForSwitchWillOverlookBlocksAndSwitchesInAnotherLayer()
    {
        AddBlockToScalar();
        var layer = (byte)Rng.Next(1, 5);
        block_.WireLayer = layer;

        var firstLoc = block_.Location - Rng.Next(5, 10);
        var first = new Switch(firstLoc);
        scalar_.Entities.Add(first);
        Assert.Null(scalar_.FindRightTargetFor(first));
        first.WireLayer = layer;
        Assert.Equal(block_, scalar_.FindRightTargetFor(first));

        var secondLoc = firstLoc - Rng.Next(5, 10);
        var second = new Switch(secondLoc);
        scalar_.Entities.Add(second);
        Assert.Null(scalar_.FindRightTargetFor(second));
        second.WireLayer = layer;
        Assert.Null(scalar_.FindRightTargetFor(second));
        first.WireLayer = (byte)(layer + 1);
        Assert.Equal(block_, scalar_.FindRightTargetFor(second));
    }

    [Fact]
    public void FindLeftTargetForSwitchWillOverlookBlocksAndSwitchesInAnotherLayer()
    {
        AddBlockToScalar();
        var layer = (byte)Rng.Next(1, 5);
        block_.WireLayer = layer;

        var firstLoc = block_.Location + Rng.Next(5, 10);
        var first = new Switch(firstLoc);
        scalar_.Entities.Add(first);
        Assert.Null(scalar_.FindLeftTargetFor(first));
        first.WireLayer = layer;
        Assert.Equal(block_, scalar_.FindLeftTargetFor(first));

        var secondLoc = firstLoc + Rng.Next(5, 10);
        var second = new Switch(secondLoc);
        scalar_.Entities.Add(second);
        Assert.Null(scalar_.FindLeftTargetFor(second));
        second.WireLayer = layer;
        Assert.Null(scalar_.FindLeftTargetFor(second));
        first.WireLayer = (byte)(layer + 1);
        Assert.Equal(block_, scalar_.FindLeftTargetFor(second));
    }

    [Fact]
    public void FindRightTargetForSwitchWillNullIfHitAnotherSwitch()
    {
        AddBlockToScalar();
        AddLeftSwitchToScalar();

        var firstLoc = leftSwitch_.Location - Rng.Next(5, 10);
        var first = new Switch(firstLoc);
        scalar_.Entities.Add(first);
        Assert.Null(scalar_.FindRightTargetFor(first));

        var secondLoc = leftSwitch_.Location + Rng.Next(2, 8);
        var second = new Switch(secondLoc);
        scalar_.Entities.Add(second);
        Assert.Equal(block_, scalar_.FindRightTargetFor(second));
    }

    [Fact]
    public void FindLeftTargetForSwitchWillNullIfHitAnotherSwitch()
    {
        AddBlockToScalar();
        AddRightSwitchToScalar();

        var firstLoc = rightSwitch_.Location + Rng.Next(5, 10);
        var first = new Switch(firstLoc);
        scalar_.Entities.Add(first);
        Assert.Null(scalar_.FindLeftTargetFor(first));

        var secondLoc = rightSwitch_.Location - Rng.Next(2, 8);
        var second = new Switch(secondLoc);
        scalar_.Entities.Add(second);
        Assert.Equal(block_, scalar_.FindLeftTargetFor(second));
    }

    private readonly TheScalar scalar_ = new();
    private readonly Character character_ = new();
    private readonly Block block_ = new(Rng.Next(110, 200));
    private readonly Switch rightSwitch_ = new(Rng.Next(210, 300));
    private readonly Switch leftSwitch_ = new(Rng.Next(50, 100));
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