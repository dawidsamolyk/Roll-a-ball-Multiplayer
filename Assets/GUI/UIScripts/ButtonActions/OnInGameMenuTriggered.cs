using UnityEngine;
using System.Collections;

public class OnInGameMenuTriggered : MonoBehaviour {
	
	public GameObject menuCamera;
	public GameObject inGameMenu;

	void Update () {

		if(PhotonNetwork.inRoom && Input.GetKey (KeyCode.Escape))
		{
			menuCamera.SetActive(true);
			NGUITools.SetActive(inGameMenu, true);
		}
	}
}
