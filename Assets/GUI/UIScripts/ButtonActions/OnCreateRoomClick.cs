using UnityEngine;
using System.Collections;

public class OnCreateRoomClick : MonoBehaviour {

	public GameObject createRoomPanel;
	public GameObject creatingRoomInfoPanel;
	public GameObject invalidParametersPanel;
	public GameObject privateRoomToggle;
	public GameObject roomNameField;
	public GameObject playerNumberField;

	private RoomOptions options;

	void Start()
	{
		//TODO default options, to implement: set options like number of max
		//players and visibility
		//options = new RoomOptions () {isVisible = true, maxPlayers = 4};
	}

	void OnClick()
	{
		string roomName = "";
		int playerNumber = 0;
		bool visible = true;

		try
		{
			roomName = getRoomName();
			playerNumber = getPlayersNumber();
		}
		catch (System.FormatException e)
		{
			setErrorPanel(e.Message);

		}
		catch (System.ArgumentException e)
		{
			setErrorPanel(e.Message);
		}

		visible = privateRoomToggle.GetActive ();
		options = new RoomOptions ()
		{isVisible = visible, maxPlayers = playerNumber};

		if (!roomName.Equals ("") && 
		    ! (playerNumber < 2 || playerNumber > 8)) {
			if (PhotonNetwork.connectedAndReady && !PhotonNetwork.inRoom) {
				CreateRoom(roomName, options);
			}
		}
	}


	/**
	 * Sets panel with information about bad parameters 
	 */
	private void setErrorPanel(string exceptionMessage)
	{
		NGUITools.SetActive(createRoomPanel, false);
		NGUITools.SetActive(invalidParametersPanel, true);
		GameObject invalidParametersPanelInfoLabel = 
			GameObject.Find ("InvalidParametersPanel/ErrorInfoLabel");

		invalidParametersPanelInfoLabel.GetComponent<UILabel> ().text = exceptionMessage;		
	}

	private int getPlayersNumber()
	{
		int playerNumber = 0;

		try
		{
			playerNumber = int.Parse(playerNumberField.GetComponent<UILabel> ().text);
		}
		catch(System.FormatException e)
		{
			throw new System.FormatException ("Invalid character in players number field.");
		}

		if(playerNumber < 2 || playerNumber > 8)
		{
			throw new System.ArgumentException("Players number is not in range 2 - 8");
		}

		return playerNumber;
	}

	private string getRoomName()
	{
		string roomName = roomNameField.GetComponent<UILabel> ().text;

		if(roomName.Equals(""))
		{
			throw new System.ArgumentException("Room name can not be empty.");
		}

		return roomName;
	}

	private void CreateRoom(string roomName, RoomOptions options)
	{
		PhotonNetwork.JoinOrCreateRoom (roomName, options, TypedLobby.Default);
		NGUITools.SetActive (createRoomPanel, false);
		NGUITools.SetActive (creatingRoomInfoPanel, true);
	}
}
