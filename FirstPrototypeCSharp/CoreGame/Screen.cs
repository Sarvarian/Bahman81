namespace CoreGame;

public class Screen
{
    public Vector2I Size { get; private set; }
    public Vector2I Center { get; private set; }
    public Vector2I Highlighter { get; private set; }

    public Screen(Vector2I initSize)
    {
        NewSize(initSize);
    }

    public void NewSize(Vector2I newSize)
    {
        Size = newSize;
        Center = Size / 2;
    }

    public static bool IsInsideAreaY(int upper, int lower, int point)
    {
        return point > upper && point < lower;
    }

    public void SetHighlighter(Vector2I highlighter)
    {
        Highlighter = highlighter;
    }

    public bool IsInsideHighlighterAreaDoubleSide(int point)
    {
        var upper = Center.Y - Highlighter.Y;
        var lower = Center.Y + Highlighter.Y;
        return IsInsideAreaY(upper, lower, point);
    }
}