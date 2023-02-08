using Godot;
using SecondPrototype.extensions;
using System;

namespace SecondPrototype.aban;

public class Grid2D
{
    public Vector2I CellSize { get; private set; }
    public Action? CellSizeUpdatedSignal;

    public Grid2D(Vector2I cellSize)
    {
        CellSize = cellSize;
    }

    public void NewCellSize(Vector2I newSize)
    {
        if (CellSize != newSize)
        {
            CellSize = newSize;
            CellSizeUpdatedSignal?.Invoke();
        }
    }

    public Vector2 LocationToPosition(int location)
    {
        return new Vector2(location * CellSize.X, 0);
    }

    public Vector2I HowManyFitsInScreen(Vector2 screenSize)
    {
        return (screenSize / CellSize).Floor().ToVec2I();
    }

    public Vector2I HowManyFitsInScreenConsideringCameraZoom(Vector2 screenSize, Vector2 cameraZoom)
    {
        return ((screenSize / cameraZoom) / CellSize).Floor().ToVec2I();
    }

    public Vector2I PositionToLocation(Vector2 position)
    {
        return (position / (Vector2)CellSize).Round().ToVec2I();
    }
}