namespace CoreGame;

public class Screen
{
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Screen(int initialWidth, int initialHeight)
    {
        Width = initialWidth;
        Height = initialHeight;
    }

    public void NewSize(int newWidth, int newHeight)
    {
        Width = newWidth;
        Height = newHeight;
    }

}