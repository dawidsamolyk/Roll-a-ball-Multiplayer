using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomList : MonoBehaviour
{

	public Matchmaker matchmaker;
	public GameObject parent;
	public GameObject roomItemModel;

	/**
	 * Distance in pixels from left borders of room items on list. 
	 */
	private const int ITEM_DISTANCE = 150;

	private const string ROOM_NAME_LABEL_NAME = "RoomNameLabel";
	private const string PLAYER_NUMBER_LABEL_NAME = "PlayersNumberLabel";
	private const string PING_LABEL_NAME = "PingLabel";

	private const string ROOM_NAME_LABEL_CONTAINS = "Room Name";
	private const string PLAYER_NUMBER_LABEL_CONTAINS = "Players";
	private const string PING_LABEL_CONTAINS = "Ping";

	void Start ()
	{
		NGUITools.SetActive (roomItemModel, false);
	}
	
	void Update ()
	{
		if (!PhotonNetwork.inRoom && matchmaker.roomsList != null) {
			int roomsNumber = matchmaker.roomsList.Length;

			List<GameObject> roomItems = new List<GameObject> ();

		//TODO poprawa usuwania pokojów
		//ustawianie pozycji elementów


			DestroyOutdatedRoomItemsFromList(roomItems);
			AddExistsRoomItemsToList(roomItems);
			RefreshPlayersCount(roomItems);
			SetValidItemsPosition(roomItems);
		}
	
	}

	/**
	 * Destroying children of UIGrid contains all rooms items
	 * may be highly unoptimized but should prevent overload
	 * of items in grid.
	 * 
	 */
	private void DestroyOutdatedRoomItemsFromList (List<GameObject> roomItems)
	{
		foreach(GameObject roomItem in roomItems)
		{
			if(IsItemOutdated(roomItem))
			{
				roomItems.Remove(roomItem);
				DestroyItem(roomItem);
			}
		}
	}

	/**
	 * Add items that don't contains in roomItems list but exist in matchmaker.roomList
	 */
	private void AddExistsRoomItemsToList(List<GameObject> roomItems)
	{
		foreach (RoomInfo roomInfo in matchmaker.roomsList) {
			if(IsNewCreatedRoom(roomInfo, roomItems))
			{
				roomItems.Add (CreateNewRoomItem(roomInfo));
			}
		}
	}


	private GameObject CreateNewRoomItem(RoomInfo info)
	{
		GameObject roomItemInstantion = Instantiate (this.roomItemModel);
		SetRoomItemLabels (roomItemInstantion, info);
		SetParent (this.parent, roomItemInstantion);
		SetActiveItem (roomItemInstantion);

		return roomItemInstantion;
	}


	/**
	 * Check if list of room items contains representations of rooms that
	 * don't exist anymore. 
	 * Return true if didn't find room in networking.roomlist with same name 
	 * as room item name. 
	 */
	private bool IsItemOutdated(GameObject item)
	{
		string roomName = GetRoomName (item);

		foreach (RoomInfo roomInfo in matchmaker.roomsList) {
			if(roomName.Equals(roomInfo.name))
			{
				return false;
			}
		}
		return true;
	}

	private bool IsNewCreatedRoom(RoomInfo roomInfo, List<GameObject> roomItems)
	{
		foreach (GameObject roomItem in roomItems) {
			if(GetRoomName(roomItem).Equals(roomInfo.name))
			{
				return false;
			}
		}

		return true;
	}

	//Get name of room from roomItem
	private string GetRoomName(GameObject roomItem)
	{
		GameObject roomNameLabel = roomItem.transform.Find (ROOM_NAME_LABEL_NAME).gameObject;
		UILabel roomNameLabelComponent = roomNameLabel.GetComponent<UILabel> ();

		return roomNameLabelComponent.text;
	}
	
	private void DestroyItem(GameObject child)
	{
		//Unactive object means is model to instantiate it.  
		if (child.GetActive ()) {
			Destroy (child);
		}
	}


	private void RefreshPlayersCount(List<GameObject> roomItems)
	{
		foreach (RoomInfo roomInfo in matchmaker.roomsList) {
			foreach(GameObject roomItem in roomItems)
			{
				if(GetRoomName(roomItem).Equals(roomInfo.name))
				{
					int currentPlayers = roomInfo.playerCount;
					int maxPlayers = roomInfo.maxPlayers;
					SetPlayersCount(currentPlayers, maxPlayers, roomItem);

					break;
				}
			}
		}
	}


	

	/**
	 * Set UILabel elements in given GameObject with info from roomInfo object.
	 * Method may be naive and silly because check if
	 * field contains given string. 
	 */
	private void SetRoomItemLabels (GameObject roomItem, RoomInfo roomInfo)
	{
		string roomName = roomInfo.name;
		int currentPlayers = roomInfo.playerCount; 
		int maxPlayers = roomInfo.maxPlayers;

		SetRoomName (roomInfo.name, roomItem);
		SetPlayersCount (currentPlayers, maxPlayers, roomItem);
	}

	private void SetRoomName(string roomName, GameObject roomItem)
	{
		GameObject roomNameLabel = roomItem.transform.Find (ROOM_NAME_LABEL_NAME).gameObject;
		roomNameLabel.GetComponent<UILabel> ().text = roomName;
	}

	private void SetPlayersCount(int currentPlayersCount, int maxPlayersCount, GameObject roomItem)
	{
		GameObject playersCountLabel = roomItem.transform.Find (PLAYER_NUMBER_LABEL_NAME).gameObject;
		playersCountLabel.GetComponent<UILabel> ().text = string.Format("Players {0}/{1}", currentPlayersCount, maxPlayersCount);
	}

	/**
	 * Set parent for kids. Don't let them go orphanage.
	 */
	private void SetParent (GameObject parent, GameObject child)
	{
		child.transform.parent = parent.transform;
	}

	/**
	 *	Set active all NGUI room items. 
	 */
	private void SetActiveItems (List<GameObject> items)
	{
		foreach (GameObject item in items) {
			SetActiveItem (item);
		}
	}

	private void SetActiveItem (GameObject item)
	{
		NGUITools.SetActive (item, true);
	}

	//
	private void SetValidItemsPosition (List<GameObject> roomItems)
	{
		int itemsCount = roomItems.Count;

		for (int i = 0; i < itemsCount; i++) {
			GameObject item = roomItems[i];
			GameObject parent = item.transform.parent.gameObject;
			setItemPosition(item, parent, i);
		}
	}

	private void setItemPosition(GameObject item, GameObject parent, int itemIndex)
	{
		Vector3 translationVector = new Vector3(itemIndex * ITEM_DISTANCE, 0f, 0f);
		item.transform.position = GetParentPosition(item);		
		item.transform.Translate(translationVector);	
		InvalidateItemTransform (item);
	}

	private void InvalidateItemTransform(GameObject item)
	{	
		Vector3 rotation = new Vector3 (0f, 0f, 0f);
		item.transform.localScale = new Vector3(1f,1f,1f);
		item.transform.rotation = new Quaternion (0f, 0f, 0f, 0f);
	}


	private Vector3 GetParentPosition(GameObject child)
	{
		return child.transform.parent.position;
	}

}
