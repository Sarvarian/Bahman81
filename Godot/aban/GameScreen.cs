using Godot;
using System;

namespace Survival.aban;

public class GameScreen
{
    public event Action? SizeUpdatedSignal;
    public event Action? PositionUpdatedSignal;

    public int Width { get; private set; }
    public int Height { get; private set; }
    public Vector2I Size => new Vector2I(Width, Height);
    public int X { get; private set; }
    public int Y { get; private set; }
    public Vector2I Start => new Vector2I(X, Y);
    public Vector2I End => new Vector2I(X + Width, Y + Height);
    public Vector2I Center => new Vector2I(X + (Width / 2), Y + (Height / 2));

    public GameScreen(Vector2I initialSize, Vector2I initialPosition)
    {
        SetNewSize(initialSize);
        SetNewPosition(initialPosition);
    }

    public void SetNewSize(Vector2I newSize)
    {
        Width = newSize.X;
        Height = newSize.Y;
        SizeUpdatedSignal?.Invoke();
    }

    public void SetNewPosition(Vector2I newPosition)
    {
        X = newPosition.X;
        Y = newPosition.Y;
        PositionUpdatedSignal?.Invoke();
    }
}
