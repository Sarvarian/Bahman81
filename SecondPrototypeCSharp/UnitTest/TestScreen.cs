using Godot;
using SecondPrototype.aban;

namespace UnitTest;

public class TestScreen : ClassTestBase
{
    [Fact]
    public void InitializeSizeCorrectly()
    {
        Assert.Equal(InitSize, screen_.Size);
    }

    [Fact]
    public void CenterPoint()
    {
        Assert.Equal(InitSize / 2, screen_.Center);
    }

    [Fact]
    public void NewSizeTest()
    {
        Assert.Equal(InitSize, screen_.Size);
        Assert.Equal(InitSize / 2, screen_.Center);

        screen_.NewSize(NewSize);

        Assert.Equal(NewSize, screen_.Size);
        Assert.Equal(NewSize / 2, screen_.Center);
    }

    [Fact]
    public void NewSizeSignal()
    {
        var counter = 0;
        screen_.SizeUpdatedSignal += () => counter += 1;

        // Screen has initial value.
        Assert.Equal(InitSize, screen_.Size);

        // If we give the same initial value to screen,
        // it does not send signal and nothing happens.
        screen_.NewSize(InitSize);
        Assert.Equal(0, counter);
        Assert.Equal(InitSize, screen_.Size);

        // When we give new value to screen.NewSize,
        // then signal will called and size will change.
        screen_.NewSize(NewSize);
        Assert.Equal(1, counter);
        Assert.Equal(NewSize, screen_.Size);
    }


    private static readonly Vector2I InitSize = RandomVector2I(100, 200, 400, 500);
    private static readonly Vector2I NewSize = RandomVector2I(700, 800, 1000, 1100);
    private readonly Screen screen_ = new(InitSize);

}