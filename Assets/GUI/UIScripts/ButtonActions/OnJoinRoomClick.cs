using UnityEngine;
using System.Collections;

public class OnJoinRoomClick : MonoBehaviour {

	public GameObject roomNameLabel;

	void OnClick()
	{
		string roomName = roomNameLabel.GetComponent<UILabel> ().text;
		PhotonNetwork.JoinRoom (roomName);
	}
}
