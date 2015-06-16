using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Ball;

public class PlayerPoints : Photon.MonoBehaviour
{
	private string guiPlayerNameAndScore;

	public GUIStyle playerNameAndScoreStyle;

	void Start ()
	{
		UpdateGuiPlayerNameAndScore ();
	}

	private void UpdateGuiPlayerNameAndScore ()
	{
		guiPlayerNameAndScore = "Player " + PhotonNetwork.player.ID + " : " + PhotonNetwork.player.GetScore() + " points";
	}

	void OnCollisionEnter (Collision col)
	{
		GameObject gameObject = col.gameObject;
		bool isCoin = gameObject.tag == "Coin";

		if (isCoin && photonView.isMine) {
			PhotonNetwork.player.AddScore (1);
			UpdateGuiPlayerNameAndScore ();
		}

		if (isCoin) {
			gameObject.SetActive (false);
			PhotonNetwork.Destroy (gameObject);
		}
	}

	void OnGUI ()
	{
		if (photonView.isMine) {
			GUI.skin.label = playerNameAndScoreStyle;
			GUILayout.Label (guiPlayerNameAndScore);
		}
	}
}
