using UnityEngine;
using System.Collections;

public class OnDisconnectClick : MonoBehaviour {
	
	void OnClick () {
		PhotonNetwork.Disconnect ();
	}
}
