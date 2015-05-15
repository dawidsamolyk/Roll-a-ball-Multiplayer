using UnityEngine;
using System.Collections;

public class OnReturnGameClick : MonoBehaviour {
	
	public GameObject menuCamera;
	public GameObject inGameMenu;
	
	void OnClick () {
		menuCamera.SetActive (false);
		NGUITools.SetActive(inGameMenu, false);
	}
}
