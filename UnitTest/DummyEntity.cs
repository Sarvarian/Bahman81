using Survival.aban.entities;

namespace UnitTest;

public class DummyEntity : Entity
{
    public DummyEntity(int location = 0)
        : base(location)
    {
    }

    public new void NewLocation(int newLocation)
    {
        base.NewLocation(newLocation);
    }
}