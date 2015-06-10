using UnityEngine;
using System.Collections;

public class NetworkingGUI : MonoBehaviour {
	public Matchmaker matchmaker;

	void Create() 
	{
		matchmaker = GetComponent<Matchmaker> ();
	}

	void OnGUI ()
	{
		DrawMainArea ();
		DrawBottomLeftArea ();
	}

	private void DrawMainArea()
	{

		if (PhotonNetwork.connectedAndReady && !PhotonNetwork.inRoom) {
			if (GUILayout.Button ("Join/create random room")) {
				PhotonNetwork.JoinRandomRoom ();
			}
			if (GUILayout.Button ("Create room: XYZ")) {
				PhotonNetwork.CreateRoom("XYZ");
			}
			
			if (!PhotonNetwork.inRoom && matchmaker.roomsList != null) {
				foreach (RoomInfo each in matchmaker.roomsList) {
					if (GUILayout.Button ("Join " + each.name)) {
						PhotonNetwork.JoinRoom (each.name);
					}    
				}
			}
		} 

		if (PhotonNetwork.connectedAndReady && PhotonNetwork.inRoom) {
			if (GUILayout.Button ("Leave room")) {
				PhotonNetwork.LeaveRoom ();
			}
		}
	}

	private void DrawBottomLeftArea() 
	{
		GUILayout.BeginArea (new Rect((Screen.width)-Screen.width*0.1f, 0 , Screen.width*0.1f, Screen.height*0.25f));
	
		if (!PhotonNetwork.connected) {
			if (GUILayout.Button ("Connect!")) {
				PhotonNetwork.ConnectUsingSettings ("0.1");				
			}
		}
		
		if (PhotonNetwork.connectedAndReady) {
			if (GUILayout.Button ("Disconnect!")) {
				PhotonNetwork.Disconnect ();
			}
			GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
		}
		
		GUILayout.EndArea();
	}

}
