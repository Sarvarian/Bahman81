using CoreGame;

namespace CoreGameTest;

public class ClassTestDummyEntity : ClassTestBase
{
    protected ClassTestDummyEntity()
    {
        Entity = new DummyEntity(DummyTickFunction);
    }
    
    protected readonly Entity Entity;
    protected int TickCallCounter { get; private set; }

    private void DummyTickFunction()
    {
        TickCallCounter += 1;
    }
}