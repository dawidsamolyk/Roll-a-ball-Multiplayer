using UnityEngine;
using System.Collections;

public class CameraRotating : MonoBehaviour {
	public float rotationSpeed;
		
	void Update () {
		float rotationAngle = rotationSpeed * Time.deltaTime;
		Vector3 rotation = new Vector3(0.0f, rotationAngle, 0.0f);

		transform.Rotate(rotation);
	}
}
