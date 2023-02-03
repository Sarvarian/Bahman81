namespace CoreGame;

public class Hunger
{
    public const int Max = 10;
    public const int CostPoint = 1;
    public const int ChargePoint = 1;

    public int Value { get; private set; } = Max;
    public bool IsZero => Value == 0;

    public event Action? ReachedZeroSignal;

    public void Cost()
    {
        if (Value > 0)
        {
            Value -= CostPoint;

            if (Value == 0)
            {
                ReachedZeroSignal?.Invoke();
            }
        }
    }

    public void Charge()
    {
        if (Value < Max)
        {
            Value += ChargePoint;
        }
    }
}