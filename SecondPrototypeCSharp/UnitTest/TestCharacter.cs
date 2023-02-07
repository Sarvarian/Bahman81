using SecondPrototype.aban.entities;

namespace UnitTest;

public class TestCharacter : ClassTestDummyEntity
{
    [Fact]
    public void CharacterIsAnEntity()
    {
        Assert.IsAssignableFrom<Entity>(character_);
    }


    private readonly Character character_ = new();
}