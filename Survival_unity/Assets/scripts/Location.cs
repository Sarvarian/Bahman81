using System.Collections;
using System.Collections.Generic;
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
		var newPos = transform.position;
		newPos.x = value_ * HardCoded.CellSize.x;
	}
	
}
