using CoreGame;

namespace CoreGameTest;

public class ClassTestBase
{
    protected readonly Random Rng = new(DateTime.Now.Millisecond);

    protected Vector2I RandomVector2I(int minX, int maxX, int minY, int maxY)
    {
        return new Vector2I(Rng.Next(minX, maxX), Rng.Next(minY, maxY));
    }

}