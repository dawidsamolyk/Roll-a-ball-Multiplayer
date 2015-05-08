using UnityEngine;
using System.Collections;

public class OnJoinRandomRoom : MonoBehaviour {

	void OnClick () {
		if (PhotonNetwork.connected && !PhotonNetwork.inRoom) {
			PhotonNetwork.JoinRandomRoom ();
		}
	}
}
