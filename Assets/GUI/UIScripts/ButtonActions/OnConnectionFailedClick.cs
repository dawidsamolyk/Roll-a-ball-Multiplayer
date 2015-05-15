using UnityEngine;
using System.Collections;

public class OnConnectionFailedClick : MonoBehaviour {

	public GameObject failedConnectionInfoPanel;
	public GameObject disconnectedPanel;


	/**
	 * Do when confirm button was clicked.
	 */
	void OnClick()
	{
		NGUITools.SetActive (failedConnectionInfoPanel, false);
		NGUITools.SetActive (disconnectedPanel, true);
	}
}
