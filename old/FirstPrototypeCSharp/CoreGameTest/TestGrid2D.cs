using CoreGame;

namespace CoreGameTest;

public class TestGrid2D : ClassTestBase
{
    [Fact]
    public void InitializeProperly()
    {
        Assert.Equal(cellSize_, grid2D_.CellSize);
    }

    public TestGrid2D()
    {
        cellSize_ = RandomVector2I(5, 10, 15, 20);
        grid2D_ = new Grid2D(cellSize_);
    }

    private readonly Vector2I cellSize_;
    private readonly Grid2D grid2D_;
}