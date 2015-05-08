using UnityEngine;
using System.Collections;

public class OnCreateRoomClick : MonoBehaviour {

	public GameObject privateRoomToggle;
	public GameObject roomNameField;

	private RoomOptions options;

	void Start()
	{
		options = new RoomOptions () {isVisible = true, maxPlayers = 4};
	}

	void OnClick()
	{
		string roomName = "";

		//roomName = privateRoomToggle.GetActive () ? "PRIVATE_" : "";
		roomName += roomNameField.GetComponent<UILabel> ().text;

		if (PhotonNetwork.connectedAndReady && !PhotonNetwork.inRoom) {
			PhotonNetwork.JoinOrCreateRoom(roomName,options,TypedLobby.Default);
		}
	}

}
