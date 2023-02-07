using Godot;
using System;

namespace SecondPrototype.aban;

public class Screen
{
    public Vector2I Size { get; private set; }
    public Vector2I Center { get; private set; }
    public event Action? SizeUpdatedSignal;

    public Screen(Vector2I size)
    {
        NewSize(size);
    }

    public void NewSize(Vector2I newSize)
    {
        if (Size != newSize)
        {
            Size = newSize;
            Center = newSize / 2;
            SizeUpdatedSignal?.Invoke();
        }
    }
}