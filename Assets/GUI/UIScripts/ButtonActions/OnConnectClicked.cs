using UnityEngine;
using System.Collections;

public class OnConnectClicked : MonoBehaviour {

	public GameObject panelToOpen;
	public GameObject panelToClose;

	//Connect user after click button
	void OnClick () {
		if ( ! PhotonNetwork.connected) {
			PhotonNetwork.ConnectUsingSettings ("0.1");
		}
	}

	//Change menu screen when user is connected
	void Update()
	{
		if (PhotonNetwork.connectedAndReady) {
			NGUITools.SetActive (panelToOpen, true);
			NGUITools.SetActive (panelToClose, false);
		}
	}
}
