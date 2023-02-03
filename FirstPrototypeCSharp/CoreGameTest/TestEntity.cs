using CoreGame;

namespace CoreGameTest;

public class TestEntity : BaseTestClass
{
    [Fact]
    public void NotNull()
    {
        Assert.NotNull(entity_);
    }

    [Fact]
    public void HasTickFunction()
    {
        entity_.Tick();
        Assert.Equal(1, tickCallCounter_);
    }

    [Fact]
    public void TickCallsPerCalls()
    {
        var reach = Rng.Next(5, 10);
        for (var i = 1; i < reach; i++)
        {
            entity_.Tick();
            Assert.Equal(i, tickCallCounter_);
        }
    }

    public TestEntity()
    {
        entity_ = new DummyEntity(DummyTickFunction);
    }
    
    private readonly Entity entity_;
    private int tickCallCounter_;

    private void DummyTickFunction()
    {
        tickCallCounter_ += 1;
    }

}