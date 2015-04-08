using UnityEngine;
using System.Collections;

public class RingRotator : MonoBehaviour {

	public float rotationSpeed;
	
	// Update is called once per frame
	void Update () {
	
		Vector3 rotation = new Vector3 (0f, 0f, 1f);

		transform.Rotate (rotation * rotationSpeed * Time.deltaTime);
	}
}
