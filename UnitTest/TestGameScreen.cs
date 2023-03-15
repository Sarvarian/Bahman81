using Godot;
using Survival.aban;

namespace UnitTest;

public class TestGameScreen : ClassTestBase
{
    [Fact]
    public void InitialValue()
    {
        Assert.Equal(InitialSize.X, screen_.Width);
        Assert.Equal(InitialSize.Y, screen_.Height);
        Assert.Equal(InitialSize, screen_.Size);
        Assert.Equal(InitialPosition.X, screen_.X);
        Assert.Equal(InitialPosition.Y, screen_.Y);
        Assert.Equal(InitialPosition, screen_.Start);
        Assert.Equal(InitialPosition + InitialSize, screen_.End);
        Assert.Equal(InitialPosition + (InitialSize / 2), screen_.Center);
    }

    [Fact]
    public void SameValueForSize()
    {
        var size = new Vector2I(screen_.Width, screen_.Height);
        Assert.Equal(size.X, screen_.Width);
        Assert.Equal(size.Y, screen_.Height);
        Assert.Equal(size, screen_.Size);
    }

    [Fact]
    public void SameValueForPosition()
    {
        var position = new Vector2I(screen_.X, screen_.Y);
        Assert.Equal(position.X, screen_.X);
        Assert.Equal(position.Y, screen_.Y);
        Assert.Equal(position, screen_.Start);
        var size = new Vector2I(screen_.Width, screen_.Height);
        Assert.Equal(position + size, screen_.End);
        Assert.Equal(position + (size / 2), screen_.Center);
    }

    [Fact]
    public void UpdateSize()
    {
        InitialValue();
        var iterTime = Rng.Next(5, 15);
        for (var i = 0; i < iterTime; i++)
        {
            var newSize = RandomVector2I(250, 260, 280, 290);
            screen_.SetNewSize(newSize);
            SameValueForSize();
            TestSize(newSize);
        }
    }

    [Fact]
    public void UpdatePosition()
    {
        InitialValue();
        var iterTime = Rng.Next(5, 15);
        for (var i = 0; i < iterTime; i++)
        {
            var newPosition = RandomVector2I(310, 320, 340, 350);
            screen_.SetNewPosition(newPosition);
            SameValueForPosition();
            TestPosition(newPosition, screen_.Size);
        }
    }

    [Fact]
    public void SizeUpdatedSignal()
    {
        var counter = 0;
        void Del()
        {
            counter += 1;
        }
        screen_.SizeUpdatedSignal += Del;
        Assert.Equal(0, counter);
        InitialValue();
        var iterTime = Rng.Next(5, 15);
        for (var i = 0; i < iterTime; i++)
        {
            var newSize = RandomVector2I(130, 140, 160, 170);
            screen_.SetNewSize(newSize);
            Assert.Equal(i + 1, counter);
            SameValueForSize();
            TestSize(newSize);
        }
        Assert.Equal(iterTime, counter);
    }

    [Fact]
    public void PositionUpdatedSignal()
    {
        var counter = 0;
        void Del()
        {
            counter += 1;
        }
        screen_.PositionUpdatedSignal += Del;
        Assert.Equal(0, counter);
        InitialValue();
        var iterTime = Rng.Next(5, 15);
        for (var i = 0; i < iterTime; i++)
        {
            var newPosition = RandomVector2I(190, 200, 220, 230);
            screen_.SetNewPosition(newPosition);
            Assert.Equal(i + 1, counter);
            SameValueForPosition();
            TestPosition(newPosition, screen_.Size);
        }
        Assert.Equal(iterTime, counter);
    }

    private static readonly Vector2I InitialSize = RandomVector2I(10, 20, 40, 50);
    private static readonly Vector2I InitialPosition = RandomVector2I(70, 80, 100, 110);
    private readonly GameScreen screen_ = new(InitialSize, InitialPosition);

    private void TestSize(Vector2I size)
    {
        Assert.Equal(size.X, screen_.Width);
        Assert.Equal(size.Y, screen_.Height);
        Assert.Equal(size, screen_.Size);
    }

    private void TestPosition(Vector2I position, Vector2I size)
    {
        Assert.Equal(position.X, screen_.X);
        Assert.Equal(position.Y, screen_.Y);
        Assert.Equal(position, screen_.Start);
        Assert.Equal(position + size, screen_.End);
        Assert.Equal(position + (size / 2), screen_.Center);
    }
}