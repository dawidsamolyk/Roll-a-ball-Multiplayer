using UnityEngine;
using System.Collections;

public class GameRulesExecutor : Photon.MonoBehaviour
{
	public GameObject extraItemsGenerator;
	public IGameOver gameOverStrategy;
	private ExtraItemsGenerator generator;
	private bool gameOver = false;
	private bool gameStarted = false;
	private bool synchronizedGameState = false;

	void Start ()
	{
		generator = extraItemsGenerator.GetComponent<ExtraItemsGenerator> ();
	}

	void Update ()
	{
		checkGameOver ();
		if (isGameOver () && synchronizedGameState) {
			gameOverStrategy.PerformGameOver ();
			gameStarted = false;
			gameOver = false;
			synchronizedGameState = false;			
		}

	}

	public void FinishGame ()
	{
		gameOver = true;
	}

	public void OnPhotonPlayerConnected (PhotonPlayer player)
	{
		if (GetRoomPlayerCount () > 1) {
			generator.generateCoins ();
			generator.generateBoosts ();
			StartGame ();
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
		if (stream.isWriting) {
			if (isGameOver ()) {
				stream.SendNext (gameOver);
				synchronizedGameState = true;
			}
		}

		if (stream.isReading) {
			if ((bool)stream.ReceiveNext ()) {
				FinishGame ();
				synchronizedGameState = true;
			}
		}
	}

	public bool isGameOver ()
	{
		return gameOver;
		//return GetRoomPlayerCount () > 1 && GetCoinCount () == 0;
	}

	public void OnGUI ()
	{
		GUILayout.BeginArea (new Rect (0, 100, 300, 300));
		GUILayout.Label ("isGameOver:" + isGameOver ());
		GUILayout.EndArea ();
	}

	private void StartGame ()
	{
		gameStarted = true;
	}

	private void checkGameOver ()
	{
		gameOver = (gameOver) ? gameOver : GetCoinCount () <= 0 && gameStarted;
	}

}
