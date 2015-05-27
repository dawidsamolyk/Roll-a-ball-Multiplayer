using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Ball;

public class PlayerPoints : Photon.MonoBehaviour
{
	private int points = 0;
	private string guiPlayerNameAndScore;

	public GUIStyle playerNameAndScoreStyle;

	void Start ()
	{
		UpdateGuiPlayerNameAndScore ();
	}

	private void UpdateGuiPlayerNameAndScore ()
	{
		guiPlayerNameAndScore = "Player " + PhotonNetwork.player.ID + " : " + points + " points";
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Coin") {
			points++;

			UpdateGuiPlayerNameAndScore ();

			col.gameObject.SetActive (false);
			PhotonNetwork.Destroy (col.gameObject);
		}
	}

	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext (points);

		} else {
			points = (int)stream.ReceiveNext ();
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
