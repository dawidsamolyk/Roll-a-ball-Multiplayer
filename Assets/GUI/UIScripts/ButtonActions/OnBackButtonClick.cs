using UnityEngine;
using System.Collections;

public class OnBackButtonClick : MonoBehaviour {

	public GameObject panelToOpen;
	public GameObject panelToClose;

	void OnClick () {
		NGUITools.SetActive (panelToOpen, true);
		NGUITools.SetActive (panelToClose, false);
	}
}
