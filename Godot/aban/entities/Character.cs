namespace Survival.aban.entities;

public class Character : Entity
{
	public enum ENextMove
	{
		Rest,
		MoveRight,
		MoveLeft
	}

	public ENextMove NextMove = ENextMove.Rest;

	public void SetLocation(int location)
	{
		NewLocation(location);
	}

	public void SetToMoveRight()
	{
		NextMove = ENextMove.MoveRight;
	}

	public void SetToMoveLeft()
	{
		NextMove = ENextMove.MoveLeft;
	}
}