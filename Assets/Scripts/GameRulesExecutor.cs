using UnityEngine;
using System.Collections;

public class GameRulesExecutor : Photon.MonoBehaviour {
	public GameObject extraItemsGenerator;
	private ExtraItemsGenerator generator;
	private bool gameOver;

	void Start () {
		generator = extraItemsGenerator.GetComponent<ExtraItemsGenerator> ();
	}

	public void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		if (GetRoomPlayerCount() > 1) {
			generator.generateCoins();
			generator.generateBoosts();
		}
	}

	private int GetRoomPlayerCount()
	{
		Room room = PhotonNetwork.room;
		
		if (room != null) {
			return room.playerCount;

		} else {
			return 0;
		}
	}

	private int GetCoinCount()
	{
		GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
		int result = coins.Length;

		foreach (GameObject eachCoin in coins) {
			// Nieaktywna moneta to zebrana moneta, więc nie należy jej liczyć
			if(eachCoin.GetActive() == false) {
				result--;
			}
		}

		return result;
	}

	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext (isGameOver());
			
		} else {
			gameOver = (bool)stream.ReceiveNext ();
		}
	}

	public bool isGameOver()
	{
		return GetRoomPlayerCount () > 1 && GetCoinCount () == 0;
	}

	public void OnGUI()
	{
		GUILayout.BeginArea (new Rect (0, 100, 300, 300));
		GUILayout.Label ("isGameOver:"+isGameOver());
		GUILayout.EndArea ();
	}
}
