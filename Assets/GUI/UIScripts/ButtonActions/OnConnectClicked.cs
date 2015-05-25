using UnityEngine;
using System.Collections;

public class OnConnectClicked : MonoBehaviour {

	public GameObject panelToOpen;
	public GameObject panelToClose;

	//If button hasn't been clicked then connect to the server.
	void OnClick () {
		if (!PhotonNetwork.connecting) {
			if (!PhotonNetwork.connected) {
				PhotonNetwork.ConnectUsingSettings ("0.1");
			}
		}
	}
}
