using UnityEngine;
using System.Collections;

public class DebugMenuController : MonoBehaviour {

	public GameObject networkingGUI;
	public GameObject fpsLimiter;

	//Set menu unactive at start
	private bool debugMenuActiveness = false;


	void Update () {
		if(Input.GetKey(KeyCode.Backslash))
		{
			debugMenuActiveness = !debugMenuActiveness;

			networkingGUI.SetActive(debugMenuActiveness);
			fpsLimiter.SetActive(debugMenuActiveness);
		}
	
	}
}
