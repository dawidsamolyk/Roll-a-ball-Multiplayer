using UnityEngine;
using System.Collections;

public class OnDisconnect : MonoBehaviour {

	public GameObject disconnectedPanel;
	public GameObject currentPanel;
	
	/**
	 * When client lost connection with PUN service, menu should
	 * back to connect panel to reconnect to server 
	 */
	void Update () {
	
		if (!PhotonNetwork.connected) {
			NGUITools.SetActive (disconnectedPanel, true);
			NGUITools.SetActive (currentPanel, false);
		}

	}
}
