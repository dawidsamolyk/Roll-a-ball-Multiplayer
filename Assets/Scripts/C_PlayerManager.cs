using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class C_PlayerManager : MonoBehaviour
{
	private PhotonView view;
	private Transform cameraPosition;

	void Start ()
	{
		view = GetComponent<PhotonView> ();
	}

	private void Awake ()
	{
		cameraPosition = Camera.main.transform;
	}

	public void Update ()
	{
		if (view.isMine) {
			float horizontalMotion = CrossPlatformInputManager.GetAxis ("Horizontal");
			float verticalMotion = CrossPlatformInputManager.GetAxis ("Vertical");
			bool jumpMotion = CrossPlatformInputManager.GetButton ("Jump");

			Vector3 camForward = Vector3.Scale (cameraPosition.forward, new Vector3 (1, 0, 1)).normalized;
			Vector3 move = (verticalMotion * camForward + horizontalMotion * cameraPosition.right).normalized;

			view.RPC ("updateClientMove", PhotonTargets.All, move, jumpMotion);
		}
	}
}
