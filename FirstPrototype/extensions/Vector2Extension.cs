namespace FirstPrototype.extensions;

public static class Vector2Extension
{
    public static Godot.Vector2I IntoGodotVector2I(this Godot.Vector2 vec)
    {
        return new Godot.Vector2I((int)vec.X, (int)vec.Y);
    }

    public static Godot.Vector2I IntoGodotVector2I(this CoreGame.Vector2I vec)
    {
        return new Godot.Vector2I(vec.X, vec.Y);
    }

    public static CoreGame.Vector2I IntoCoreVector2I(this Godot.Vector2I vec)
    {
        return new CoreGame.Vector2I(vec.X, vec.Y);
    }
}