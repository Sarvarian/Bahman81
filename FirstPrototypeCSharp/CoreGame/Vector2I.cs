namespace CoreGame;

public readonly record struct Vector2I
{
    public readonly int X;
    public readonly int Y;

    public Vector2I(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Vector2I operator /(Vector2I dividend, int divisor)
    {
        return new Vector2I(dividend.X / divisor, dividend.Y / divisor);
    }

}