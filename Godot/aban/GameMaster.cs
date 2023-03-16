using Survival.aban.entities;

namespace Survival.aban;

public class GameMaster
{
	public GameMaster(TheScalar scalar)
	{
		scalar_ = scalar;
		player_ = new Character();
		scalar.Entities.Add(player_);
	}

	private readonly TheScalar scalar_;
	private readonly Character player_;

	public Character GetPlayer()
	{
		return player_;
	}
}