using CoreGame;

namespace CoreGameTest;

public class TestCharacter
{
    [Fact]
    public void CharacterIsAnEntity()
    {
        Assert.IsAssignableFrom<Entity>(character_);
    }

    [Fact]
    public void HasHealth()
    {
        Assert.IsType<Health>(character_.Health);
    }

    [Fact]
    public void HasHunger()
    {
        Assert.IsType<Hunger>(character_.Hunger);
    }



    private readonly Character character_ = new();
}