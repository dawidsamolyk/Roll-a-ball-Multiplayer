using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnResultsTriggered : MonoBehaviour
{
	public KeyCode resultsKey;
	public GameObject resultsPanel;

	void Update()
	{
		if (Input.GetKeyDown (resultsKey)) {
			NGUITools.SetActive (resultsPanel, true);
		} else if (Input.GetKeyUp (resultsKey)) {
			NGUITools.SetActive (resultsPanel, false);
		}
	}
}

