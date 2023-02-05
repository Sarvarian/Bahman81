using CoreGame;

namespace CoreGameTest;

public class TestScreen : ClassTestBase
{
    [Fact]
    public void ScreenSizeInitialized()
    {
        Assert.Equal(initSize_, screen_.Size);
    }

    [Fact]
    public void NewSize()
    {
        Assert.Equal(initSize_, screen_.Size);
        screen_.NewSize(newSize_);
        Assert.Equal(newSize_, screen_.Size);
    }

    [Fact]
    public void CenterPoint()
    {
        Assert.Equal(initSize_ / 2, screen_.Center);
    }

    public TestScreen()
    {
        initSize_ = new Vector2I(Rng.Next(1, 100), Rng.Next(101, 200));
        screen_ = new(initSize_);
        newSize_ = new Vector2I(Rng.Next(301, 400), Rng.Next(401, 500));
    }

    private readonly Vector2I initSize_;
    private readonly Vector2I newSize_;
    private readonly Screen screen_;
}