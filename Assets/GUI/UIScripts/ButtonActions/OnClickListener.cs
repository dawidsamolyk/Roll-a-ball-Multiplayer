using UnityEngine;
using System.Collections;

public class OnClickListener : MonoBehaviour {

	public UIInput input;

	void OnPress (bool isPressed)
	{
		if (isPressed) {
			string text = input.text;
			Debug.Log("PRESSED!");
			Debug.Log("TEXT: " + text);
		}
	}
}
