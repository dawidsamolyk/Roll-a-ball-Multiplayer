using UnityEngine;
using System.Collections;

public class OnInGameMenuTriggered : MonoBehaviour {

	public KeyCode menuKey;
	public GameObject menuCamera;
	public GameObject inGameMenu;

	void Update () {

		if(PhotonNetwork.inRoom && Input.GetKey (menuKey))
		{
			menuCamera.SetActive(true);
			NGUITools.SetActive(inGameMenu, true);
		}
	}
}
