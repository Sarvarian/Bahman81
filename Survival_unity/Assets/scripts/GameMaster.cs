using UnityEngine;

[DisallowMultipleComponent]
public class GameMaster : MonoBehaviour
{
	[SerializeField] private GameObject characterPrefab;

	private void Start ()
	{
		var player = Instantiate(characterPrefab);
		player.GetComponent<Location>().Set(0);
		player.AddComponent<PlayerController>();
	}
}
