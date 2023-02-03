using CoreGame;

namespace CoreGameTest;

public class TestCharacter
{
    [Fact]
    public void CharacterIsAnEntity()
    {
        Assert.IsAssignableFrom<Entity>(character_);
    }

    private readonly Character character_ = new();
}