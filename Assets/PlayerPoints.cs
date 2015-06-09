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
		if (col.gameObject.tag == "Coin" && photonView.isMine) {
			PhotonNetwork.player.AddScore(1);

			UpdateGuiPlayerNameAndScore ();

			col.gameObject.SetActive (false);
			PhotonNetwork.Destroy (col.gameObject);
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
