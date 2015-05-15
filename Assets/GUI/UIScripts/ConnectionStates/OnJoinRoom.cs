using UnityEngine;
using System.Collections;

public class OnJoinRoom : MonoBehaviour {
	
	public GameObject currentPanel;
	public GameObject playerCamera;
	public GameObject menuCamera;


	/**
	 * When player is in room, started game, menu should disappear
	 * and player should see game plan.
	 * 
	 */
	void Update()
	{
		if (PhotonNetwork.inRoom) {
			NGUITools.SetActive(currentPanel, false);
			menuCamera.SetActive(false);
			playerCamera.SetActive(true);
			
		}
	}
}
