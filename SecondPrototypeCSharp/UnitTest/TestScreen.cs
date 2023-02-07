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


    private static readonly Vector2I InitSize = RandomVector2I(10, 20, 40, 50);
    private readonly Screen screen_ = new(InitSize);

}