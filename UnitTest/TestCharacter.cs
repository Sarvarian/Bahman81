using Survival.aban;
using Survival.aban.entities;

namespace UnitTest;

public class TestCharacter : ClassTestBase
{
	[Fact]
	public void CharacterIsAnEntity()
	{
		Assert.IsAssignableFrom<Entity>(character_);
	}

	[Fact]
	public void SetLocation()
	{
		Assert.Equal(0, character_.Location);
		var newLocation = Rng.Next(5, 10);
		character_.SetLocation(newLocation);
		Assert.Equal(newLocation, character_.Location);
	}

	[Fact]
	public void SetToMoveRight()
	{
		Assert.Equal(
			Character.ENextMove.Rest,
			character_.NextMove
		);
		character_.SetToMoveRight();
		Assert.Equal(
			Character.ENextMove.MoveRight,
			character_.NextMove
		);
	}

	[Fact]
	public void SetToMoveLeft()
	{
		Assert.Equal(
			Character.ENextMove.Rest,
			character_.NextMove
		);
		character_.SetToMoveLeft();
		Assert.Equal(
			Character.ENextMove.MoveLeft,
			character_.NextMove
		);
	}

	[Fact]
	public void HasHealth()
	{
		Assert.IsAssignableFrom<int>(character_.Health);
	}

	[Fact]
	public void CanSetHealth()
	{
		var healthInitialValue = character_.Health;
		Assert.Equal(healthInitialValue, character_.Health);
		var i = Rng.Next(5, 10);
		for (; i > 0; i--)
		{
			var newHealth = Rng.Next(100, 1000);
			character_.SetHealth(newHealth);
			Assert.Equal(newHealth, character_.Health);
		}
	}


	public TestCharacter()
	{
		scalar_.Entities.Add(character_);
	}

	private readonly Character character_ = new();
	private readonly TheScalar scalar_ = new();

}