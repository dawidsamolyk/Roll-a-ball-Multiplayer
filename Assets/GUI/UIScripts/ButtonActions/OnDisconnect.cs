using UnityEngine;
using System.Collections;

public class OnDisconnect : MonoBehaviour {

	public GameObject panelToOpen;
	public GameObject panelToClose;

	
	// Update is called once per frame
	void Update () {
	
		if (! PhotonNetwork.connected) {
			NGUITools.SetActive (panelToOpen, true);
			NGUITools.SetActive (panelToClose, false);
		}

	}
}
