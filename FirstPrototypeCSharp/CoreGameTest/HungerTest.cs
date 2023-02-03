using CoreGame;

namespace CoreGameTest;

public class HungerTest
{
    [Fact]
    public void InitialValue()
    {
        Assert.Equal(Hunger.Max, hunger_.Value);
    }

    [Fact]
    public void Cost()
    {
        hunger_.Cost();
        Assert.Equal(Hunger.Max - Hunger.CostPoint, hunger_.Value);
    }

    [Fact]
    public void CostNoMoreThenZero()
    {
        CostOverZero();
        Assert.Equal(0, hunger_.Value);
    }

    [Fact]
    public void IsZero()
    {
        CostTillZero();
        Assert.True(hunger_.IsZero);
    }

    [Fact]
    public void ReachedZeroSignal()
    {
        var eventCallNumber = 0;
        hunger_.ReachedZeroSignal += () => eventCallNumber += 1;
        CostOverZero();
        Assert.Equal(1, eventCallNumber);
    }

    [Fact]
    public void Charge()
    {
        CostNoZero();
        var h = hunger_.Value;
        hunger_.Charge();
        Assert.Equal(h + Hunger.ChargePoint, hunger_.Value);
    }

    [Fact]
    public void ChargeNoMoreThenMax()
    {
        ChargeOverMax();
        Assert.Equal(Hunger.Max, hunger_.Value);
    }

    private readonly Hunger hunger_ = new();
    private readonly Random rnd_ = new(DateTime.Now.Millisecond);

    private void CostNoZero()
    {
        var i = rnd_.Next(1, Hunger.Max - 1);
        for (; i > 0; i--)
        {
            hunger_.Cost();
        }
    }

    private void CostTillZero()
    {
        for (var i = 0; i < Hunger.Max; i++)
        {
            hunger_.Cost();
        }
    }

    private void CostOverZero()
    {
        var i = rnd_.Next(Hunger.Max + 1, Hunger.Max + 5);
        for (; i > 0; i--)
        {
            hunger_.Cost();
        }
    }

    private void ChargeOverMax()
    {
        var i = rnd_.Next(Hunger.Max + 1, Hunger.Max + 5);
        for (; i > 0; i--)
        {
            hunger_.Charge();
        }
    }

}