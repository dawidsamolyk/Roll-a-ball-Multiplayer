using UnityEngine;
using System.Collections;

public class GameRulesExecutor : Photon.MonoBehaviour
{
	public GameObject extraItemsGenerator;
	public IGameOver gameOverStrategy;
	private ExtraItemsGenerator generator;
	private bool gameOver = false;
	private bool gameStarted = false;

	void Start ()
	{
		generator = extraItemsGenerator.GetComponent<ExtraItemsGenerator> ();
	}

	void Update ()
	{
		if (isGameOver ()) {
			FinishGame ();
		}
	}

	public void FinishGame ()
	{
		hideRoom ();
		GameOver ();
	}

	public void OnPhotonPlayerConnected (PhotonPlayer player)
	{
		if (GetRoomPlayerCount () > 1) {
			generator.generateCoins ();
			generator.generateBoosts ();
		}
	}

	private int GetRoomPlayerCount ()
	{
		Room room = PhotonNetwork.room;
		
		if (room != null) {
			return room.playerCount;

		} else {
			return 0;
		}
	}

	private int GetCoinCount ()
	{
		GameObject[] coins = GameObject.FindGameObjectsWithTag ("Coin");
		int result = coins.Length;

		foreach (GameObject eachCoin in coins) {
			// Nieaktywna moneta to zebrana moneta, więc nie należy jej liczyć
			if (eachCoin.GetActive () == false) {
				result--;
			}
		}

		return result;
	}

	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if (gameOver) {
			if (stream.isWriting) {
				stream.SendNext (gameOver);
			}
		} else {
			if (stream.isReading) {
				gameOver = (bool)stream.ReceiveNext ();
			}
		}
	}

	public bool isGameOver ()
	{
		StartGame ();
		return gameStarted && 
			GetCoinCount () <= 0;
	}

	/*public void OnGUI ()
	{
		GUILayout.BeginArea (new Rect (0, 100, 300, 300));
		GUILayout.Label ("isGameOver: " + isGameOver ());
		GUILayout.Label ("Coins remaining " + GetCoinCount ());
		GUILayout.Label ("Players in room " + GetRoomPlayerCount ());
		GUILayout.Label ("Game started " + gameStarted);
		GUILayout.EndArea ();
	}*/

	private void StartGame ()
	{
		if (GetRoomPlayerCount () > 1 && GetCoinCount () > 0) {
			gameStarted = true;
		}
	}

	private void GameOver ()
	{
		gameOverStrategy.PerformGameOver ();
		gameStarted = false;
		gameOver = false;
	}

	private void hideRoom ()
	{
		PhotonNetwork.room.open = false;
		PhotonNetwork.room.visible = false;
	}


}
