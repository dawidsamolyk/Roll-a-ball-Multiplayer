using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnResultsTriggered : MonoBehaviour
{

	public KeyCode resultsKey;
	public GameObject resultsPanel;
	public GameObject playerModel;


	private List<GameObject> playerItems;
	private PhotonPlayer[] players;

	void Start()
	{
		playerItems = new List<GameObject> ();
	}


	void Update ()
	{
		resultsPanelActivity ();
	}

	private void resultsPanelActivity ()
	{
		if (PhotonNetwork.inRoom && Input.GetKey (resultsKey)) {
			SetPlayerList();
			NGUITools.SetActive (resultsPanel, true);

		} else {
			NGUITools.SetActive (resultsPanel, false);
			ClearPlayers();
		}	
	}


	private void SetPlayerList()
	{
		players = PhotonNetwork.playerList;
		AddPlayersToList ();
		ActivePlayerItems ();
	}

	private void AddPlayersToList()
	{
		GameObject item;
		foreach (PhotonPlayer player in players) {
			item = Instantiate(playerModel);
			SetLabelsInItem(player, item);
			playerItems.Add(item);		
		}
	}

	private void SetLabelsInItem(PhotonPlayer player, GameObject playerItem)
	{
		UILabel nickLabel, pointsLabel, pingLabel;
		nickLabel = getLabelFromChild (player, LabelNames.NICK_LABEL_NAME);
		pointsLabel = getLabelFromChild (player, LabelNames.POINTS_LABEL_NAME);
		pingLabel = getLabelFromChild (player, LabelNames.PING_LABEL_NAME);

		nickLabel.text = player.name;
		pointsLabel.text = player.GetScore ();
		pingLabel.text = "TO BE IMPLEMENTED";
	}

	private UILabel getLabelFromChild(GameObject parent, string childName)
	{
		UILabel label = parent.transform.FindChild (childName).gameObject.GetComponent<UILabel> ();
		return label;
	}

	private void ActivePlayerItems()
	{
		foreach (GameObject item in playerItems) {
			NGUITools.SetActive(item, true);
		}
	}


	private void ClearPlayers()
	{
		foreach (GameObject item in playerItems) {
			Destroy(item);		
		}
	}

}

class LabelNames
{
	public const string NICK_LABEL_NAME = "PlayerNickLabel";
	public const string POINTS_LABEL_NAME = "PlayerPointsLabel";
	public const string PING_LABEL_NAME = "PingLabel";
}

