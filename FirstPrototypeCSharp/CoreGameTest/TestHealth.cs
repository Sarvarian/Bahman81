using CoreGame;

namespace CoreGameTest;

public class TestHealth
{
    [Fact]
    public void InitialValue()
    {
        Assert.Equal(Health.Max, health_.Value);
    }

    [Fact]
    public void Damage()
    {
        health_.Damage();
        Assert.Equal(Health.Max - Health.DamageCost, health_.Value);
    }

    [Fact]
    public void DamageNoMoreThenZero()
    {
        DamageOverDeath();
        Assert.Equal(0, health_.Value);
    }

    [Fact]
    public void Recovery()
    {
        DamageNoDeath();
        var hp = health_.Value;
        health_.Recover();
        Assert.Equal(hp + Health.RecoverPoint, health_.Value);
    }

    [Fact]
    public void RecoveryNoMoreThenMaxValue()
    {
        RecoverOverMax();
        Assert.Equal(Health.Max, health_.Value);
    }

    [Fact]
    public void IsDead()
    {
        DamageTillDeath();
        Assert.True(health_.IsDead);
    }

    [Fact]
    public void DeathSignal()
    {
        var eventCallNumber = 0;
        health_.DeathSignal += () => eventCallNumber += 1;
        DamageOverDeath();
        Assert.Equal(1, eventCallNumber);
    }

    private readonly Health health_ = new();
    private readonly Random rng_ = new(DateTime.Now.Millisecond);

    private void DamageNoDeath()
    {
        var i = rng_.Next(1, Health.Max - 1);
        for (; i > 0; i--)
        {
            health_.Damage();
        }
    }

    private void DamageTillDeath()
    {
        for (var i = 0; i < Health.Max; i++)
        {
            health_.Damage();
        }
    }

    private void DamageOverDeath()
    {
        var i = rng_.Next(Health.Max + 1, Health.Max + 5);
        for (; i > 0; i--)
        {
            health_.Damage();
        }
    }

    private void RecoverOverMax()
    {
        var i = rng_.Next(Health.Max + 1, Health.Max + 5);
        for (; i > 0; i--)
        {
            health_.Recover();
        }
    }

}