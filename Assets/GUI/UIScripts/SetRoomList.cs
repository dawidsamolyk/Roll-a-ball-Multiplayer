using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetRoomList : MonoBehaviour
{

	public Matchmaker matchmaker;
	public GameObject parent;
	public GameObject roomItem;

	void Start ()
	{
		NGUITools.SetActive (roomItem, false);
	}

	

	//Update room list on every frame
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

			destroyChildren (parent);
			setAllRoomItems (roomItems, roomsNumber);
			setParent (parent, roomItems);
			setItemsPosition(roomItems);
			setActiveItems (roomItems);
		}
	
	}

	/**
	 * Destroying children of UIGrid contains all rooms items
	 * may be highly unoptimized but should prevent overload
	 * of items in grid.
	 * 
	 */
	private void destroyChildren (GameObject parent)
	{
		int childrenAmount = parent.transform.childCount;

		if (childrenAmount > 0) {
			for (int i = childrenAmount - 1; i >= 0; i--) {
				Destroy (parent.transform.GetChild (i).gameObject);
			}
		}
	}

	/**
	 * Set UILabel elements in given GameObject with info from roomInfo object.
	 * Method may be naive and silly because check if
	 * field contains given string. 
	 */
	private void setRoomItem (GameObject roomItem, RoomInfo roomInfo)
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
	private void setAllRoomItems (List <GameObject> roomItems, int roomsNumber)
	{
		for (int i = 0; i < roomsNumber; i++) {
			GameObject roomItemCopy = Instantiate (this.roomItem); 
			roomItems.Add (roomItemCopy);
			setRoomItem (roomItemCopy, matchmaker.roomsList [i]);
		}
	}

	/**
	 * Set parent for kids. Don't let them go orphanage.
	 */
	private void setParent (GameObject parent, List<GameObject> children)
	{
		foreach (GameObject child in children) {
			child.transform.parent = parent.transform;		
		}
	}

	/**
	 *	Set active all NGUI room items. 
	 */
	private void setActiveItems (List<GameObject> items)
	{
		foreach (GameObject item in items) {
			NGUITools.SetActive (item, true);		
		}
	}

	private void setItemsPosition(List<GameObject> roomItems)
	{
		int itemShift = 150; //how much pixels to right put next item
		int size = roomItems.Count;

		for (int i=0; i < size; i++) {
			roomItems[i].transform.position = new Vector3(i * itemShift, 0f, 0f);
		}
	}


}
