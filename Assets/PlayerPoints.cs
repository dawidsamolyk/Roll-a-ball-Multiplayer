using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Ball;

public class PlayerPoints : MonoBehaviour {
	private int points = 0;

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Coin")
		{
			points++;

			col.gameObject.SetActive(false);
			PhotonNetwork.Destroy(col.gameObject);
		}
	}

	public void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if(stream.isWriting) {
			stream.SendNext(points);
		}
		else {
			points = (int)stream.ReceiveNext();
		}
	}
}
