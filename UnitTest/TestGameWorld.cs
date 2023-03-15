using Godot;
using Survival.aban;

namespace UnitTest;

public class TestGameWorld : ClassTestBase
{
    [Fact]
    public void InitialValue()
    {
        Assert.Equal(InitialOffset, world_.Offset);
    }

    [Fact]
    public void UpdateOffset()
    {
        InitialValue();
        var iterTimes = Rng.Next(5, 15);
        for (var i = 0; i < iterTimes; i++)
        {
            var newOffset = RandomVector2I(130, 140, 160, 170);
            world_.SetNewOffset(newOffset);
            Assert.Equal(newOffset, world_.Offset);
        }
    }

    [Fact]
    public void OffsetUpdatedSignal()
    {
        var counter = 0;
        void Del()
        {
            counter += 1;
        }
        world_.OffsetUpdatedSignal += Del;
        Assert.Equal(0, counter);
        InitialValue();
        var iterTimes = Rng.Next(5, 15);
        for (var i = 0; i < iterTimes; i++)
        {
            var newOffset = RandomVector2I(70, 80, 100, 110);
            world_.SetNewOffset(newOffset);
            Assert.Equal(i + 1, counter);
            Assert.Equal(newOffset, world_.Offset);
        }
        Assert.Equal(iterTimes, counter);
    }


    private static readonly Vector2I InitialOffset = RandomVector2I(10, 20, 40, 50);
    private readonly GameWorld world_ = new(InitialOffset);

}