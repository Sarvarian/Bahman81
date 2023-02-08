using Godot;

namespace UnitTest;

public class ClassTestBase
{
    protected static readonly Random Rng = new(DateTime.Now.Millisecond);

    protected static Vector2I RandomVector2I(int minX, int maxX, int minY, int maxY)
    {
        return new Vector2I(Rng.Next(minX, maxX), Rng.Next(minY, maxY));
    }

    protected static Vector2 RandomVector2(float minX, float maxX, float minY, float maxY)
    {
        var rangeX = maxX - minX;
        var rangeY = maxY - minY;
        var x = (Rng.NextSingle() * rangeX) + minX;
        var y = (Rng.NextSingle() * rangeY + minY);
        return new Vector2(x, y);
    }

}

public class TestClassTestBase : ClassTestBase
{
    [Fact]
    public void TestRandomVector2()
    {
        const float minX = 100.0f;
        const float maxX = 200.0f;
        const float minY = 300.0f;
        const float maxY = 400.0f;

        var result = RandomVector2(minX, maxX, minY, maxY);

        Assert.True(result.X > minX);
        Assert.True(result.X < maxX);
        Assert.True(result.Y > minY);
        Assert.True(result.Y < maxY);
    }

}