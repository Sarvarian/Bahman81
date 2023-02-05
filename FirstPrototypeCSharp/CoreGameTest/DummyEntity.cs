using CoreGame;

namespace CoreGameTest;

public class DummyEntity : Entity
{
    public DummyEntity(Action tickFunction, int location = 0)
        : base(location)
    {
        tickFunction_ = tickFunction;
    }

    public override void Tick() => tickFunction_();

    private readonly Action tickFunction_;

}