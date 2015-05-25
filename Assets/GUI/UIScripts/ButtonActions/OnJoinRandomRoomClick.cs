using UnityEngine;
using System.Collections;

public class OnJoinRandomRoomClick : MonoBehaviour {

	void OnClick () {
			PhotonNetwork.JoinRandomRoom ();
	}
}
