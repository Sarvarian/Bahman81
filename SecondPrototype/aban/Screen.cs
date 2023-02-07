using Godot;

namespace SecondPrototype.aban;

public readonly record struct Screen(Vector2I Size)
{
    public readonly Vector2I Center = Size / 2;
}