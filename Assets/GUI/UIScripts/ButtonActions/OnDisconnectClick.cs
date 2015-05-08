using UnityEngine;
using System.Collections;

public class OnDisconnectClick : MonoBehaviour {

	
	// Update is called once per frame
	void OnClick () {
		PhotonNetwork.Disconnect ();
	}
}
