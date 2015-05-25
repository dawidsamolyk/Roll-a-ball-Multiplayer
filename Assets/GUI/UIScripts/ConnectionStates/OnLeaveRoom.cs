using UnityEngine;
using System.Collections;

public class OnLeaveRoom : MonoBehaviour {

	public GameObject inGameMenuPanel;
	public GameObject mainMenuPanel;

	
	// Back to main menu when is not in room anymore.
	void Update () {
		if (!PhotonNetwork.inRoom) {
			NGUITools.SetActive(inGameMenuPanel, false);
			NGUITools.SetActive(mainMenuPanel, true);
		}	
	}
}
