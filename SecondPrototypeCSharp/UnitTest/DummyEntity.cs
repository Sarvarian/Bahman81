using SecondPrototype.aban.entities;

namespace UnitTest;

public class DummyEntity : Entity
{
    public DummyEntity(Action tickFunction, int location = 0)
        : base(location)
    {
        tickFunction_ = tickFunction;
    }

    public new void NewLocation(int newLocation)
    {
        base.NewLocation(newLocation);
    }

    public override void Tick() => tickFunction_();

    private readonly Action tickFunction_;
}