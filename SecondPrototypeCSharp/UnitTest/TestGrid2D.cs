using Godot;
using SecondPrototype.aban;

namespace UnitTest;

public class TestGrid2D : ClassTestBase
{
    [Fact]
    public void CellSizeInitializedProperly()
    {
        Assert.Equal(CellSize, grid2D_.CellSize);
    }

    private static readonly Vector2I CellSize = RandomVector2I(10, 20, 40, 50);
    private readonly Grid2D grid2D_ = new(CellSize);

}