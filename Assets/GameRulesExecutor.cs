using UnityEngine;
using System.Collections;

public class GameRulesExecutor : MonoBehaviour {
	public GameObject extraItemsGenerator;
	private ExtraItemsGenerator generator;
	private int lastPlayersCount = 0;

	void Start () {
		generator = extraItemsGenerator.GetComponent<ExtraItemsGenerator> ();
	}

	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Room room = PhotonNetwork.room;

		if (room != null && room.playerCount > 1) {
			generator.generateCoins();
			generator.generateBoosts();
		}
	}
}
