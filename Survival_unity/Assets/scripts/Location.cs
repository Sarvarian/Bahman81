using UnityEngine;

[DisallowMultipleComponent]
public class Location : MonoBehaviour
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
		UpdateLocation();
	}

	private int value_;

	private void UpdateLocation()
	{
		var myTransform = transform;
		var newPos = myTransform.position;
		newPos.x = value_ * HardCoded.CellSize.x;
		newPos.x += HardCoded.WorldOrigin.x;
		myTransform.position = newPos;
	}
	
}
