using CoreGame;

namespace CoreGameTest;

public class TestScreen : ClassTestBase
{
    [Fact]
    public void ScreenSizeInitialized()
    {
        Assert.Equal(initWidth_, screen_.Width);
        Assert.Equal(initHeight_, screen_.Height);
    }

    [Fact]
    public void NewSize()
    {
        Assert.Equal(initWidth_, screen_.Width);
        Assert.Equal(initHeight_, screen_.Height);

        screen_.NewSize(newWidth_, newHeight_);

        Assert.Equal(newWidth_, screen_.Width);
        Assert.Equal(newHeight_, screen_.Height);
    }

    [Fact]
    public void CenterPoint()
    {
        Assert.Equal(initWidth_ / 2, screen_.CenterX);
        Assert.Equal(initHeight_ / 2, screen_.CenterY);
    }


    public TestScreen()
    {
        initWidth_ = Rng.Next(1, 100);
        initHeight_ = Rng.Next(101, 200);
        newWidth_ = Rng.Next(301, 400);
        newHeight_ = Rng.Next(401, 500);
        screen_ = new(initWidth_, initHeight_);
    }

    private readonly int initWidth_;
    private readonly int initHeight_;
    private readonly int newWidth_;
    private readonly int newHeight_;
    private readonly Screen screen_;
}