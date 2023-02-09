using Survival.aban;
using Survival.aban.entities;

namespace UnitTest;

public class TestTheScalar : ClassTestBase
{
	[Fact]
	public void HasEntityList()
	{
		Assert.IsType<List<Entity>>(scalar_.Entities);
	}

	[Fact]
	public void EntityListIsNotNull()
	{
		Assert.NotNull(scalar_.Entities);
	}

	[Fact]
	public void HasTick()
	{
		AddCharacterToWorld();

		var reach = Rng.Next(5, 10);
		for (var i = 1; i < reach; i++)
		{
			character_.SetToMoveRight();
			scalar_.Tick();
			Assert.Equal(i, character_.Location);
		}
	}

	[Fact]
	public void EntitiesInLocation()
	{
		AddCharacterToWorld();
		var expected = new Entity[] { character_ };
		Assert.Equal(expected, scalar_.EntitiesAt(0));
	}


	[Fact]
	public void SetToMoveRight()
	{
		AddCharacterToWorld();

		// On initial location is 0.
		Assert.Equal(0, character_.Location);

		// Calling Tick will not change location.
		scalar_.Tick();
		Assert.Equal(0, character_.Location);

		// Calling MoveRight also will not change location.
		character_.SetToMoveRight();
		Assert.Equal(0, character_.Location);

		// But calling Tick after MoveRight will change location.
		scalar_.Tick();
		Assert.Equal(1, character_.Location);

		// Again calling Tick after tick will not change location.
		scalar_.Tick();
		Assert.Equal(1, character_.Location);
	}

	[Fact]
	public void SetToMoveLeft()
	{
		AddCharacterToWorld();

		Assert.Equal(0, character_.Location);

		scalar_.Tick();
		Assert.Equal(0, character_.Location);

		character_.SetToMoveLeft();
		Assert.Equal(0, character_.Location);

		scalar_.Tick();
		Assert.Equal(-1, character_.Location);

		scalar_.Tick();
		Assert.Equal(-1, character_.Location);
	}

	private readonly TheScalar scalar_ = new();
	private readonly Character character_ = new();

	private void AddCharacterToWorld()
	{
		scalar_.Entities.Add(character_);
	}

}