using Godot;

namespace SecondPrototype.extensions;

public static class Vector2Extension
{
    public static Vector2I ToVec2I(this Vector2 vector2)
    {
        return (Vector2I)vector2;
    }

}