using UnityEngine;

public class GameMaster : MonoBehaviour
{
	// [SerializeField] private GameObject characterPrefab;
	[SerializeField] private GameObject playerPrefab;

	private void Start ()
	{
		var player = Instantiate(playerPrefab);
		player.GetComponent<Location>().Set(0);
	}
}
