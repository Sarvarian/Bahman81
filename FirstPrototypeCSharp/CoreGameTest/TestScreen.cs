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

    [Fact]
    public void YAreaDetection()
    {
        var ySize = screen_.Size.Y;
        var yDividedBy10 = ySize / 10;
        var upperMax = yDividedBy10 * 2;
        var upper = Rng.Next(yDividedBy10, upperMax);
        var lowerMin = ySize - (yDividedBy10 * 2);
        var lowerMax = ySize - yDividedBy10;
        var lower = Rng.Next(lowerMin, lowerMax);
        
        var point1 = Rng.Next(upper + 1, lower - 1);
        Assert.True(point1 > upper);
        Assert.True(point1 < lower);

        var point2 = Rng.Next(1, upper - 1);
        Assert.True(point2 < upper);
        Assert.True(point2 < lower);
        
        var point3 = Rng.Next(lower + 1, ySize - 1);
        Assert.True(point3 > upper);
        Assert.True(point3 > lower);
        
        Assert.True(Screen.IsInsideAreaY(upper, lower, point1));
        Assert.False(Screen.IsInsideAreaY(upper, lower, point2));
        Assert.False(Screen.IsInsideAreaY(upper, lower, point3));
    }


    public TestScreen()
    {
        initSize_ = new Vector2I(Rng.Next(100, 101), Rng.Next(201, 300));
        screen_ = new(initSize_);
        newSize_ = new Vector2I(Rng.Next(401, 500), Rng.Next(501, 600));
    }

    private readonly Vector2I initSize_;
    private readonly Vector2I newSize_;
    private readonly Screen screen_;
}