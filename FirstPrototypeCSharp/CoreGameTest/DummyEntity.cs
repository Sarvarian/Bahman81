using CoreGame;

namespace CoreGameTest;

public class DummyEntity : Entity
{
    public DummyEntity(Action tickFunction)
    {
        tickFunction_ = tickFunction;
    }

    public override void Tick() => tickFunction_();

    private readonly Action tickFunction_;

}