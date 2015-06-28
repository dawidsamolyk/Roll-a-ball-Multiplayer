using UnityEngine;
using System.Collections;

public class OnFinishGameButtonClick : MonoBehaviour
{

	public GameObject panelToShow;
	public GameObject resultsPanel;
	public GameObject playerCamera;
	public GameObject menuCamera;

	void Update()
	{
		if (!PhotonNetwork.inRoom) {
			//playerCamera.SetActive (false);
			menuCamera.SetActive (true);

			NGUITools.SetActive(resultsPanel,false);
			NGUITools.SetActive(panelToShow, true);
		}
	}

}
