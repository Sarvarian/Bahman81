using Godot;
using System;

namespace Survival.aban;

public class GameWorld
{
    public event Action? OffsetUpdatedSignal;

    /// <summary>
    /// Offset from Godot world. 
    /// </summary>
    public Vector2I Offset { get; private set; }

    public GameWorld(Vector2I initialOffset)
    {
        SetNewOffset(initialOffset);
    }

    public void SetNewOffset(Vector2I newOffset)
    {
        Offset = newOffset;
        OffsetUpdatedSignal?.Invoke();
    }

}