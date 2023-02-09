using Survival.aban.entities;

namespace UnitTest;

public class ClassTestDummyEntity : ClassTestBase
{
    protected ClassTestDummyEntity()
    {
        Dummy = new DummyEntity(DummyTickFunction);
    }

    protected readonly Entity Dummy;
    protected int TickCallCounter { get; private set; }

    protected void DummyTickFunction()
    {
        TickCallCounter += 1;
    }

}