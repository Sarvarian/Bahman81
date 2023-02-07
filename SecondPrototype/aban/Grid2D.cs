using Godot;
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
}