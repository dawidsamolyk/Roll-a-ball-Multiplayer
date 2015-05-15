using UnityEngine;
using System.Collections;

public class OnConnected : MonoBehaviour {

	public GameObject connectingPanel;
	public GameObject menuPanel;

	void Update()
	{
		if(PhotonNetwork.connected || PhotonNetwork.connectedAndReady)
		{
			NGUITools.SetActive(connectingPanel, false);
			NGUITools.SetActive(menuPanel, true);
		}
	}
}
