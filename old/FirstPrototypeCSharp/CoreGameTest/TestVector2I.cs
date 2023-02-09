using CoreGame;

namespace CoreGameTest;

public class TestVector2I : ClassTestBase
{
    [Fact]
    public void InitializedProperly()
    {
        Assert.Equal(initialX_, sample_.X);
        Assert.Equal(initialY_, sample_.Y);
    }

    [Fact]
    public void DivisionOperator()
    {
        var divisor = Rng.Next(1, 10);
        var result = sample_ / divisor;
        Assert.Equal(sample_.X / divisor, result.X);
        Assert.Equal(sample_.Y / divisor, result.Y);
    }


    public TestVector2I()
    {
        initialX_ = Rng.Next(100, 200);
        initialY_ = Rng.Next(201, 300);
        sample_ = new Vector2I(initialX_, initialY_);
    }

    private readonly int initialX_;
    private readonly int initialY_;
    private readonly Vector2I sample_;
}