using UnityEngine;
using System.Collections;

public class OnJoinRoom : MonoBehaviour {
	
	public GameObject panelToClose;
	public GameObject playerCamera;
	public GameObject menuCamera;

	
	void Update()
	{
		if (PhotonNetwork.inRoom) {
			NGUITools.SetActive(panelToClose, false);
			menuCamera.SetActive(false);
			playerCamera.SetActive(true);
			
		}
	}
}
