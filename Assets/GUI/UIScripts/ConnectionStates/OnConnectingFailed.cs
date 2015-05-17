using UnityEngine;
using System.Collections;

public class OnConnectingFailed : MonoBehaviour {

	public GameObject connectingFailedInfoPanel;
	public GameObject connectingPanel;
	
	/**
	 * In connecting panel, when connection failed, player should see
	 * info about connection failed. 
	 */

	void Update () {
		if (!PhotonNetwork.connecting && !PhotonNetwork.connected) {
			NGUITools.SetActive(connectingPanel, false);
			NGUITools.SetActive(connectingFailedInfoPanel, true);
		}	
	}
}
