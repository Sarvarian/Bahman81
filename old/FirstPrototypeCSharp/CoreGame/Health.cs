namespace CoreGame;

public class Health
{
    public const int Max = 10;
    public const int DamageCost = 1;
    public const int RecoverPoint = 1;

    public int Value { get; private set; } = Max;
    public bool IsDead => Value == 0;

    public event Action? DeathSignal;

    public void Damage()
    {
        if (Value > 0)
        {
            Value -= DamageCost;

            if (Value == 0)
            {
                DeathSignal?.Invoke();
            }
        }
    }

    public void Recover()
    {
        if (Value < Max)
        {
            Value += RecoverPoint;
        }
    }

}