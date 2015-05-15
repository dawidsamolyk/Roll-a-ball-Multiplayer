using UnityEngine;
using System.Collections;

public class OnCreateRoomClick : MonoBehaviour {

	public GameObject createRoomPanel;
	public GameObject creatingRoomInfoPanel;
	public GameObject privateRoomToggle;
	public GameObject roomNameField;

	private RoomOptions options;

	void Start()
	{
		//TODO default options, to implement: set options like number of max
		//players and visibility
		options = new RoomOptions () {isVisible = true, maxPlayers = 4};
	}

	void OnClick()
	{
		string roomName = "";

		options.isVisible = roomNameField.GetActive ();
		roomName = roomNameField.GetComponent<UILabel> ().text;

		if (!roomName.Equals ("")) {
			if (PhotonNetwork.connectedAndReady && !PhotonNetwork.inRoom) {
				PhotonNetwork.JoinOrCreateRoom (roomName, options, TypedLobby.Default);
				NGUITools.SetActive (createRoomPanel, false);
				NGUITools.SetActive (creatingRoomInfoPanel, true);
			}
		}
	}

}
