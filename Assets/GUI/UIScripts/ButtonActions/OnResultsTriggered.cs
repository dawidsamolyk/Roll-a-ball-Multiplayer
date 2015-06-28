using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnResultsTriggered : MonoBehaviour
{
	public KeyCode resultsKey;
	public GameObject resultsPanel;

	private const string BUTTON_NAME = "LeaveRoomButton";

	void Update()
	{
		if (!isEndGame()) {
			if (Input.GetKeyDown (resultsKey)) {
				NGUITools.SetActive (resultsPanel, true);
			} else if (Input.GetKeyUp (resultsKey)) {
				NGUITools.SetActive (resultsPanel, false);
			}
		}
	}

	//If end game button is active, disallow to trigger menu by pressing button.
	private bool isEndGame()
	{
		Transform child = transform.FindChild (BUTTON_NAME);

		if (child != null) {
			return child.gameObject.GetActive();		
		} else {
			return false;
		}
	}

}

