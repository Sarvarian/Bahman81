namespace CoreGame;

public class Screen
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int CenterX { get; private set; }
    public int CenterY { get; private set; }

    public Screen(int initialWidth, int initialHeight)
    {
        NewSize(initialWidth, initialHeight);
    }

    public void NewSize(int newWidth, int newHeight)
    {
        Width = newWidth;
        Height = newHeight;
        CenterX = Width / 2;
        CenterY = Height / 2;
    }

}