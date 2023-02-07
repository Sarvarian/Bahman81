using Godot;

namespace SecondPrototype.aban;

public class Screen
{
    public Vector2I Center { get; }
    public Vector2I Size { get; }

    public Screen(Vector2I size)
    {
        Size = size;
        Center = size / 2;
    }

}