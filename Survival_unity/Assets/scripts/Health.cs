using UnityEngine;

public class Health : MonoBehaviour
{
	public int Value
	{
		get
		{
			return Get;
		}
		set
		{
			Set(value);
		}
	}

	public int Get
	{
		get
		{
			return value_;
		}
	}

	public void Set(int newValue)
	{
		value_ = newValue;
	}

	public void Decrement(int amount = 1)
	{
		Set(value_ - amount);
	}

	private int value_ = 10;

}
