using Godot;
using Survival.aban;
using Survival.extensions;

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

    [Fact]
    public void LocationToPosition()
    {
        var randomLocation = 0;
        while (randomLocation == 0) { randomLocation = Rng.Next(-10, 10); }
        var expectedResult = new Vector2
        {
            X = randomLocation * grid_.CellSize.X,
            Y = 0
        };
        Assert.Equal(expectedResult, grid_.LocationToPosition(randomLocation));
    }

    [Fact]
    public void HowManyFitsInScreen()
    {
        var screenSize = RandomVector2(100.0f, 200.0f, 400.0f, 500.0f);

        var expected = new Vector2I
        {
            X = Mathf.FloorToInt(screenSize.X / grid_.CellSize.X),
            Y = Mathf.FloorToInt(screenSize.Y / grid_.CellSize.Y)
        };

        Assert.Equal(expected, grid_.HowManyFitsInScreen(screenSize));
    }

    [Fact]
    public void HowManyFitsInScreenConsideringCameraZoom()
    {
        var screenSize = RandomVector2(100, 200, 400, 500);
        var cameraZoom = RandomVector2(0.1f, 2.0f, 0.1f, 2.0f);

        var expectedF = new Vector2
        {
            X = (screenSize.X / cameraZoom.X) / grid_.CellSize.X,
            Y = (screenSize.Y / cameraZoom.Y) / grid_.CellSize.Y
        };

        var expectedI = expectedF.Floor().ToVec2I();
        Assert.Equal(expectedI, grid_.HowManyFitsInScreenConsideringCameraZoom(screenSize, cameraZoom));
    }

    [Fact]
    public void PositionToLocation()
    {
        var position = RandomVector2(10.0f, 20.0f, 40.0f, 50.0f);
        var locFloat = position / (Vector2)grid_.CellSize;
        var locInt = locFloat.Round().ToVec2I();
        var expected = locInt;
        Assert.Equal(expected, grid_.PositionToLocation(position));
    }

    [Fact]
    public void Vector2ILocationToPosition()
    {
        var randomLocation = RandomVector2I(10, 20, 40, 50);
        var expected = new Vector2
        {
            X = randomLocation.X * grid_.CellSize.X,
            Y = randomLocation.Y * grid_.CellSize.Y
        };
        Assert.Equal(expected, grid_.LocationToPosition(randomLocation));
    }



    private static readonly Vector2I InitCellSize = RandomVector2I(10, 20, 40, 50);
    private static readonly Vector2I NewCellSize = RandomVector2I(70, 80, 100, 110);
    private readonly Grid2D grid_ = new(InitCellSize);

}