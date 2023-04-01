using UnityEngine;

[RequireComponent(typeof(Location))]
public class PlayerController : MonoBehaviour
{
	private Location location_;

	private void Start()
	{
		location_ = GetComponent<Location>();
	}

	private void Update()
	{
		var loc = location_.Get;
		var newLoc = loc + GetDir();
		if (loc != newLoc)
		{
			location_.Set(newLoc);
		}
	}

	private int GetDir()
	{
		var dir = 0;
		
		if (IsGoLeft())
		{
			dir -= 1;
		}

		if (IsGoRight())
		{
			dir += 1;
		}

		return dir;
	}

	private bool IsGoLeft()
	{
		return Input.GetKeyDown(KeyCode.LeftArrow);
	}
	
	private bool IsGoRight()
	{
		return Input.GetKeyDown(KeyCode.RightArrow);
	}
	
	
}
