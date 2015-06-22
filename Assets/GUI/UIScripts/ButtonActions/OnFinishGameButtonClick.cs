using UnityEngine;
using System.Collections;

public class OnFinishGameButtonClick : MonoBehaviour {

	public GameObject panelToShow;
	public GameObject panelToHide;
	public GameObject playerCamera;
	public GameObject menuCamera;

	void OnClick()
	{
		//first disactives itself in order to not showing this again in new game
		//after display results in-game
		NGUITools.SetActive (gameObject, false);
		NGUITools.SetActive (panelToHide, false);
		NGUITools.SetActive (playerCamera, false);
		NGUITools.SetActive (menuCamera, true);
		NGUITools.SetActive (panelToShow, true);
	}
}
