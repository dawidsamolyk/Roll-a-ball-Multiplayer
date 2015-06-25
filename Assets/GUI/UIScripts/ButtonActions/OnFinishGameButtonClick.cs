using UnityEngine;
using System.Collections;

public class OnFinishGameButtonClick : MonoBehaviour
{

	public GameObject panelToShow;
	public GameObject panelToHide;
	public GameObject playerCamera;
	public GameObject menuCamera;

	void OnClick ()
	{
		PhotonNetwork.LeaveRoom ();
	}

	void Update()
	{
		if (!PhotonNetwork.inRoom) {
			playerCamera.SetActive (false);
			gameObject.SetActive (false);
			panelToHide.SetActive (false);
		
			menuCamera.SetActive (true);
			panelToShow.SetActive (true);
		}
	}

}
