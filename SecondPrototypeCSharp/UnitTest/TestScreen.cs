using Godot;
using SecondPrototype.aban;

namespace UnitTest;

public class TestScreen : ClassTestBase
{
    [Fact]
    public void InitializeSizeCorrectly()
    {
        Assert.Equal(Size, screen_.Size);
    }

    private static readonly Vector2I Size = RandomVector2I(10, 20, 40, 50);
    private readonly Screen screen_ = new(Size);

}