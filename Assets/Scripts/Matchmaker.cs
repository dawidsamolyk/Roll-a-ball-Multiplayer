using UnityEngine;

public class Matchmaker : Photon.MonoBehaviour
{
	public RoomInfo[] roomsList;
	public SpawnPoint spawnPoints;

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
		ball.transform.position = spawnPoints.GetSpawnPosition (PhotonNetwork.player.ID);
	}

	public bool containsRoom (string roomName)
	{
		foreach (RoomInfo info in roomsList) {
			if (info.name.Equals (roomName)) {
				return true;
			}		
		}		
		return false;
	}

	public RoomInfo getRoomByName (string name)
	{
		foreach (RoomInfo room in roomsList) {
			if (room.name.Equals (name)) {
				return room;
			}		
		}

		return null;
	}

}
