namespace CoreGame;

public class Screen
{
    public Vector2I Size { get; private set; }
    public Vector2I Center { get; private set; }
    public Vector2I Highlighter { get; private set; }
    public int PixelPerGroundRulerStep { get; private set; }

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

    public void SetPixelPerGroundRulerStep(int pixelPerStep)
    {
        PixelPerGroundRulerStep = pixelPerStep;
    }

    public int LocationOfPointInGroundRuler(int point)
    {
#if DEBUG
        if (PixelPerGroundRulerStep == 0)
        {
            throw new exceptions.PixelPerGroundRulerStepIsZero();
        }
#endif
        var distanceToCenter = point - Center.X;
        var floatPositionOnRuler = distanceToCenter / (float)PixelPerGroundRulerStep;
        var location = Convert.ToInt32(floatPositionOnRuler);
        return location;
    }
}