using UnityEngine;

public class Location : MonoBehaviour
{
	public int Value
	{
		get
		{
			return value_;
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
		UpdateLocation();
	}

	private int value_;

	private void UpdateLocation()
	{
		var myTransform = transform;
		var newPos = myTransform.position;
		newPos.x = value_ * HardCoded.CellSize.x;
		myTransform.position = newPos;
	}
	
}
