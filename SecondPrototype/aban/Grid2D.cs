using Godot;

namespace SecondPrototype.aban;

public class Grid2D
{
    public Vector2I CellSize { get; }

    public Grid2D(Vector2I cellSize)
    {
        CellSize = cellSize;
    }
}