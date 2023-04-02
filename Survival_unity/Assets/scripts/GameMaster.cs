using UnityEngine;

[DisallowMultipleComponent]
public class GameMaster : MonoBehaviour
{
	[SerializeField] private GameObject characterPrefab;
	[SerializeField] private GameObject barPrefab;

	private void Start ()
	{
		var player = Instantiate(characterPrefab);
		player.GetComponent<Location>().Set(0);
		player.AddComponent<PlayerController>();

		var bar = Instantiate(barPrefab, player.transform, true);
	}
}
