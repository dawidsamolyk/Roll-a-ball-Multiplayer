using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetRoomList : MonoBehaviour
{

	public Matchmaker matchmaker;
	public GameObject parent;
	public GameObject roomItem;

	/**
	 * Distance in pixels from left borders of room items on list. 
	 */
	private const int ITEM_DISTANCE = 150;

	void Start ()
	{
		NGUITools.SetActive (roomItem, false);
	}
	
	void Update ()
	{
		if (!PhotonNetwork.inRoom && matchmaker.roomsList != null) {
			int roomsNumber = matchmaker.roomsList.Length;

			List<GameObject> roomItems = new List<GameObject> ();

			/*	workflow
			 	 * usuń istniejące dzieci w siatce
				 * stwórz nowe instancje, 
				 * poustawiaj wartości pól,
				 * ustaw rodzica,
				 * uaktywnij
				 */ 

			DestroyChildren (parent);
			ClearItemsList(roomItems);
			InstantiateRoomItems (roomItems, roomsNumber);
			SetParent (parent, roomItems);
			SetItemsPosition (roomItems);
			//setActiveItems (roomItems);
		}
	
	}

	/**
	 * Destroying children of UIGrid contains all rooms items
	 * may be highly unoptimized but should prevent overload
	 * of items in grid.
	 * 
	 */
	private void DestroyChildren (GameObject parent)
	{
		int childCount = parent.transform.childCount;

		if (childCount > 0) {
			for (int i = childCount - 1; i >= 0; i--) {
				GameObject child = parent.transform.GetChild(i).gameObject;
				DestroyChild(child);
			}
		}
	}

	private void DestroyChild(GameObject child)
	{
		//Unactive object means is model to instantiate it.  
		if (child.GetActive ()) {
			Destroy (child);
		}
	}

	private void ClearItemsList(List<GameObject> itemList)
	{
		itemList.Clear();
	}

	/**
	 * Set UILabel elements in given GameObject with info from roomInfo object.
	 * Method may be naive and silly because check if
	 * field contains given string. 
	 */
	private void SetRoomItem (GameObject roomItem, RoomInfo roomInfo)
	{
		List<UILabel> labels = new List<UILabel> ();
		roomItem.GetComponentsInChildren<UILabel> (true, labels);

		foreach (UILabel label in labels) {
			string text = label.text;

			if (text.Contains ("Room Name")) {
				text = roomInfo.name;
			} else if (text.Contains ("Players")) {
				text = "Players " 
					+ roomInfo.playerCount + " / " + roomInfo.maxPlayers;
			} else if (text.Contains ("Ping")) {
				text = "Ping";
			}

			label.text = text;		
		}
	}

	/**
	 * Set strings in labels for all items that must be created.
	 * Each item is a copy of original item roomItem.
	 * Put all copies to list for later processing. 
	 */
	private void InstantiateRoomItems (List <GameObject> roomItems, int roomsCount)
	{
		for (int i = 0; i < roomsCount; i++) {
			GameObject roomItemCopy = Instantiate (this.roomItem); 
			roomItems.Add (roomItemCopy);
			SetRoomItem (roomItemCopy, matchmaker.roomsList [i]);
			SetActiveItem (roomItemCopy);
		}
	}

	/**
	 * Set parent for kids. Don't let them go orphanage.
	 */
	private void SetParent (GameObject parent, List<GameObject> children)
	{
		foreach (GameObject child in children) {
			child.transform.parent = parent.transform;		
		}
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
	private void SetItemsPosition (List<GameObject> roomItems)
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
