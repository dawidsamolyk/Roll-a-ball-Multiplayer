using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnResultsTriggered : MonoBehaviour
{

	public KeyCode resultsKey;
	public GameObject resultsPanel;
	public GameObject playerModel;
	public GameObject itemsParent;
	public UIGrid grid;
	private List<GameObject> playersItems;

	void Start ()
	{
		playersItems = new List<GameObject> ();
	}

	void Update ()
	{
		resultsPanelActivity ();
	}

	private void resultsPanelActivity ()
	{
		if (PhotonNetwork.inRoom && Input.GetKeyDown (resultsKey)) {
			EnablePanel (resultsPanel);
			NGUITools.SetActive (resultsPanel, true);

		} else if (Input.GetKeyUp (resultsKey)) {
			DisablePanel (resultsPanel);
		}	
	}

	private void EnablePanel (GameObject panel)
	{
		PreparePlayersList ();
		NGUITools.SetActive (panel, true);
	}

	private void DisablePanel (GameObject panel)
	{
		NGUITools.SetActive (panel, false);
		ClearItems (playersItems);
	}

	private void PreparePlayersList ()
	{
		PhotonPlayer[] players = PhotonNetwork.playerList;
		playersItems = PhotonPlayersToList (players);
		//SetParents (playersItems, itemsParent);
		//ValidateItemsTransform (playersItems);
		ActivePlayerItems (playersItems);
		Reposition ();
	}

	private List<GameObject> PhotonPlayersToList (PhotonPlayer[] photonPlayers)
	{
		GameObject item;
		List<GameObject> items = new List<GameObject> ();

		foreach (PhotonPlayer player in photonPlayers) {
			item = NGUITools.AddChild (itemsParent, playerModel);
			SetLabelsInItem (player, item);
			items.Add (item);
		}

		return items;
	}

	private void SetLabelsInItem (PhotonPlayer player, GameObject playerItem)
	{
		UILabel nickLabel, pointsLabel;
		GameObjectHelper helper = new GameObjectHelper ();

		nickLabel = helper.GetComponentFromChild<UILabel> (playerItem, LabelNames.NICK_LABEL_NAME); 
		pointsLabel = helper.GetComponentFromChild<UILabel> (playerItem, LabelNames.POINTS_LABEL_NAME);

		nickLabel.text = string.Format("Player{0}",player.ID);
		pointsLabel.text = string.Format ("{0} pts", player.GetScore ());

		MarkLocalPlayer(player, playerItem);			

	}

	private void ActivePlayerItems (List<GameObject> items)
	{
		foreach (GameObject item in items) {
			NGUITools.SetActive (item, true);
		}
	}

	private void MarkLocalPlayer(PhotonPlayer player, GameObject playerItem)
	{
		if (player == PhotonNetwork.player) {
			UILabel nickLabel, pointsLabel;
			GameObjectHelper helper = new GameObjectHelper ();
		
			nickLabel = helper.GetComponentFromChild<UILabel> (playerItem, LabelNames.NICK_LABEL_NAME); 
			pointsLabel = helper.GetComponentFromChild<UILabel> (playerItem, LabelNames.POINTS_LABEL_NAME);
		
			nickLabel.color = Color.yellow;
			pointsLabel.color = Color.yellow;
		}

	}


	private void SetParents (List<GameObject> items, GameObject parent)
	{
		GameObjectHelper helper = new GameObjectHelper ();
		foreach (GameObject child in items) {
			helper.setParent (child, parent);
		}
	}
	
	private void ValidateItemsTransform (List<GameObject> items)
	{
		GameObjectHelper helper = new GameObjectHelper ();
		foreach (GameObject item in items) {
			TransformValidator.ResetItemTransform (item);
			helper.setTransformFromParent (item);			
		}
	}

	private void ClearItems (List<GameObject> items)
	{
		foreach (GameObject item in items) {
			NGUITools.Destroy (item);
		}
		items.Clear ();		
	}

	private void Reposition ()
	{
		grid.repositionNow = true;
	}

}

class LabelNames
{
	public const string NICK_LABEL_NAME = "PlayerNickLabel";
	public const string POINTS_LABEL_NAME = "PlayerPointsLabel";
	public const string PING_LABEL_NAME = "PingLabel";
}

