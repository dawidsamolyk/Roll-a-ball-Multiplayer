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
			GameOver();
		}
	}

	public void FinishGame ()
	{
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
		return (GetCoinCount () <= 0 
		        && GetRoomPlayerCount() > 1);
	}

	public void OnGUI ()
	{
		GUILayout.BeginArea (new Rect (0, 100, 300, 300));
		GUILayout.Label ("isGameOver: " + isGameOver ());
		GUILayout.Label ("Coins remaining " + GetCoinCount());
		GUILayout.Label ("Players in room " + GetRoomPlayerCount());
		GUILayout.EndArea ();
	}

	private void StartGame ()
	{
		gameStarted = true;
	}

	private void GameOver()
	{
		gameOverStrategy.PerformGameOver ();
		gameStarted = false;
		gameOver = false;
	}

}
