using UnityEngine;

public class Matchmaker : Photon.MonoBehaviour
{
	private PhotonView myPhotonView;
	public RoomInfo[] roomsList;

	// Use this for initialization
	void Start ()
	{
		PhotonNetwork.ConnectUsingSettings ("0.1");
	}

	void OnPhotonRandomJoinFailed ()
	{
		PhotonNetwork.CreateRoom (null);
	}

	void OnReceivedRoomListUpdate ()
	{
		roomsList = PhotonNetwork.GetRoomList ();
	}

	void OnJoinedRoom ()
	{
		GameObject ball = PhotonNetwork.Instantiate ("RollerBall", Vector3.zero, Quaternion.identity, 0);
		myPhotonView = ball.GetComponent<PhotonView> ();

		// BEGIN - przykład zachowywania dodatkowych parametrów w pokoju gry
		var table = new ExitGames.Client.Photon.Hashtable ();
		table.Add ("Points", 3);
		PhotonNetwork.room.SetCustomProperties (table);

		foreach (string each in PhotonNetwork.room.customProperties.Keys) {
			UnityEngine.Debug.Log (each);
		}
		// END - przykład zachowywania dodatkowych parametrów w pokoju gry
	}
	

}
