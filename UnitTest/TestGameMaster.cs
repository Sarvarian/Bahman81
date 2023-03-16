using Survival.aban;

namespace UnitTest;

public class TestGameMaster : ClassTestBase
{
	[Fact]
	public void AddACharacterAsPlayerOnInitialization()
	{
		TheScalar myScalar = new();
		Assert.Empty(myScalar.Entities);
		GameMaster myGameMaster = new(myScalar);
		Assert.Single(myScalar.Entities);
		Assert.Equal(myGameMaster.GetPlayer(), myScalar.Entities[0]);
	}

	public TestGameMaster()
	{
		gameMaster_ = new(scalar_);
	}

	private readonly TheScalar scalar_ = new();
	private readonly GameMaster gameMaster_;
}