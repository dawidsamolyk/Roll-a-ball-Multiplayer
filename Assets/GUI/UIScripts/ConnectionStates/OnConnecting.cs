using UnityEngine;
using System.Collections;

public class OnConnecting : MonoBehaviour {

	public GameObject connectingPanel;
	public GameObject disconnectedPanel;
		
	/**
	 * When player is connecting to the server, should see 
	 * panel with information that says player is curently 
	 * connecting to server 
	 */
	void Update () {
		if (PhotonNetwork.connecting) {
			NGUITools.SetActive(connectingPanel,true);
			NGUITools.SetActive(disconnectedPanel,false);
		}
	}
}
