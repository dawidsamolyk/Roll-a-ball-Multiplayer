using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnEnableResultsPanel : MonoBehaviour
{

	public GameObject resultsPanel;
	public GameObject playerModel;
	public GameObject itemsParent;
	public UIGrid grid;


	void Start ()
	{
	}

	void OnEnable ()
	{
		ClearItems ();
		EnablePanel (resultsPanel);
	}

	void OnDisable ()
	{
		DisablePanel (resultsPanel);
	}

	void Update ()
	{
	}

	private void EnablePanel (GameObject panel)
	{
		PreparePlayersList ();
	}
	
	private void DisablePanel (GameObject panel)
	{
		ClearItems ();
	}
	
	private void PreparePlayersList ()
	{
		PhotonPlayer[] players = PhotonNetwork.playerList;

		foreach (PhotonPlayer player in players) {
			SetPlayerItem (player);
		}


		//playersItems = PhotonPlayersToList (players);
		//SetParents (playersItems, itemsParent);
		//ValidateItemsTransform (playersItems);
		//ActivePlayerItems (playersItems);
		Reposition ();
	}
	
	private void SetPlayerItem (PhotonPlayer photonPlayer)
	{
		GameObject item;
		item = NGUITools.AddChild (itemsParent, playerModel);
		SetLabelsInItem (photonPlayer, item);
		ActivePlayerItem (item);
	}
	
	private void SetLabelsInItem (PhotonPlayer player, GameObject playerItem)
	{
		UILabel nickLabel, pointsLabel;
		GameObjectHelper helper = new GameObjectHelper ();
		
		nickLabel = helper.GetComponentFromChild<UILabel> (playerItem, LabelNames.NICK_LABEL_NAME); 
		pointsLabel = helper.GetComponentFromChild<UILabel> (playerItem, LabelNames.POINTS_LABEL_NAME);
		
		nickLabel.text = string.Format ("Player{0}", player.ID);
		pointsLabel.text = string.Format ("{0} pts", player.GetScore ());
		
		MarkLocalPlayer (player, playerItem);			
		
	}
	
	private void ActivePlayerItem (GameObject item)
	{
		NGUITools.SetActive (item, true);
	}
	
	private void MarkLocalPlayer (PhotonPlayer player, GameObject playerItem)
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
		
	private void ValidateItemsTransform (List<GameObject> items)
	{
		GameObjectHelper helper = new GameObjectHelper ();
		foreach (GameObject item in items) {
			TransformValidator.ResetItemTransform (item);
			helper.setTransformFromParent (item);			
		}
	}
	
	private void ClearItems ()
	{
		int childCount = itemsParent.transform.childCount;

		for(int counter = 0; counter < childCount; counter++)
		{
			GameObject childGameObject = itemsParent.transform.GetChild(counter).gameObject;

			if (childGameObject.name.Contains("Clone")) {
				NGUITools.Destroy (childGameObject);
			}
		}
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
