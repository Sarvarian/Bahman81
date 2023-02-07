using Godot;
using SecondPrototype.aban;

namespace UnitTest;

public class TestGrid2D : ClassTestBase
{
    [Fact]
    public void CellSizeInitializedProperly()
    {
        Assert.Equal(InitCellSize, grid_.CellSize);
    }

    [Fact]
    public void NewCellSizeTest()
    {
        Assert.Equal(InitCellSize, grid_.CellSize);
        grid_.NewCellSize(NewCellSize);
        Assert.Equal(NewCellSize, grid_.CellSize);
    }

    [Fact]
    public void NewCellSizeSignalTest()
    {
        var counter = 0;
        grid_.CellSizeUpdatedSignal += () =>
        {
            Assert.Equal(NewCellSize, grid_.CellSize);
            counter += 1;
        };

        // Grid cell size has initial value.
        Assert.Equal(InitCellSize, grid_.CellSize);

        // If we give the same value to grid cell size,
        // it does not send signal and nothing happens.
        grid_.NewCellSize(InitCellSize);
        Assert.Equal(0, counter);
        Assert.Equal(InitCellSize, grid_.CellSize);

        // When we give a new value to grid cell size,
        // then size will change and signal will call.
        grid_.NewCellSize(NewCellSize);
        Assert.Equal(1, counter);
        Assert.Equal(NewCellSize, grid_.CellSize);
    }



    private static readonly Vector2I InitCellSize = RandomVector2I(10, 20, 40, 50);
    private static readonly Vector2I NewCellSize = RandomVector2I(70, 80, 100, 110);
    private readonly Grid2D grid_ = new(InitCellSize);

}