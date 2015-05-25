using UnityEngine;
using System.Collections;

public class OnLeaveRoomClick : MonoBehaviour {

	void OnClick()
	{
		PhotonNetwork.LeaveRoom();
	}

}
