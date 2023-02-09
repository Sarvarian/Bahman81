using Godot;

namespace FirstPrototype;

public record DebugDrawGroundRuler(CanvasItem Canvas, CoreGame.Screen Screen, int PixelPerStep)
{
    public void Draw()
    {
        BaseLine();
        NumberLines();
    }

    private readonly Color color_ = Colors.DarkRed;

    private void BaseLine()
    {
        Canvas.DrawLine(
            new Vector2(0, Screen.Center.Y),
            new Vector2(Screen.Size.X, Screen.Center.Y),
            color_
        );
    }

    private void NumberLines()
    {
        SingleNumberLine(0);
        var maxStep = Screen.Center.X / PixelPerStep;
        for (var i = 1; i < maxStep; i++)
        {
            SingleNumberLine(i * 1);
            SingleNumberLine(i * -1);
        }
    }

    private void SingleNumberLine(int location)
    {
        var xn = Screen.Center.X + (location * PixelPerStep);
        Canvas.DrawLine(
            new Vector2(xn, Screen.Center.Y),
            new Vector2(xn, Screen.Center.Y + 10),
            color_
        );
    }

}