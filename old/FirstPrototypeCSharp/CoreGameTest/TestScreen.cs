using CoreGame;
using CoreGame.exceptions;

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

    [Fact]
    public void SetHighlighter()
    {
        var highlighter = CreateHighlighter();
        screen_.SetHighlighter(highlighter);
        Assert.Equal(highlighter, screen_.Highlighter);
    }

    [Fact]
    public void IsInsideHighlighterAreaDoubleSide()
    {
        CreateAndSetHighlighter();

        var upper = screen_.Center.Y - screen_.Highlighter.Y;
        var lower = screen_.Center.Y + screen_.Highlighter.Y;

        var point1 = Rng.Next(upper + 1, lower - 1);
        var point2 = Rng.Next(1, upper - 1);
        var point3 = Rng.Next(lower + 1, screen_.Size.Y);

        Assert.True(screen_.IsInsideHighlighterAreaDoubleSide(point1));
        Assert.False(screen_.IsInsideHighlighterAreaDoubleSide(point2));
        Assert.False(screen_.IsInsideHighlighterAreaDoubleSide(point3));
    }

    [Fact]
    public void ExceptionOnLocationOfPointInGroundRulerIfPixelPerGroundRulerStepIsZero()
    {
        var point = Rng.Next(1, screen_.Size.X - 1);
        Assert.Throws<PixelPerGroundRulerStepIsZero>(() =>
            screen_.LocationOfPointInGroundRuler(point)
        );
        var pps = Rng.Next(1, 10);
        screen_.SetPixelPerGroundRulerStep(pps);
        screen_.LocationOfPointInGroundRuler(point);
    }


    [Fact]
    public void SetPixelPerGroundRulerStep()
    {
        var pps = Rng.Next(1, 10);
        screen_.SetPixelPerGroundRulerStep(pps);
        Assert.Equal(pps, screen_.PixelPerGroundRulerStep);
    }

    [Fact]
    public void LocationOfPointInGroundRuler()
    {
        CreateAndSetHighlighter();
        SetPixelPerGroundRulerStep();

        var point1 = Rng.Next(1, screen_.Size.X - 1);
        var expected = (int)Math.Round(
            (point1 - screen_.Center.X)
            / (float)screen_.PixelPerGroundRulerStep
        );

        Assert.Equal(expected, screen_.LocationOfPointInGroundRuler(point1));
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

    private Vector2I CreateHighlighter()
    {
        var width = Rng.Next(5, 10);
        var height = Rng.Next(11, 20);
        var highlighter = new Vector2I(width, height);
        return highlighter;
    }

    private void CreateAndSetHighlighter()
    {
        var highlighter = CreateHighlighter();
        screen_.SetHighlighter(highlighter);
    }

}