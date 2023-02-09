using Survival.aban.entities;

namespace UnitTest;

public class MockEntity : Entity
{
    public MockEntity(int location = 0)
        : base(location)
    {
    }

    public new void NewLocation(int newLocation)
    {
        base.NewLocation(newLocation);
    }
}