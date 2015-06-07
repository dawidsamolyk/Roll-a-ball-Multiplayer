using UnityEngine;
using System.Collections;

public class GameObjectHelper {

	public T GetComponentFromChild<T>(GameObject parent, string childName)
	{
		GameObject child = parent.transform.FindChild (childName).gameObject;
		return child.GetComponent<T> ();
	}

	public void setParent(GameObject child, GameObject parent)
	{
		child.transform.parent = parent.transform;
	}

	public void setTransformFromParent(GameObject child)
	{
		GameObject parent = child.transform.parent.gameObject;
		setTransformFromParent (child, parent);
	}

	public void setTransformFromParent(GameObject child, GameObject parent)
	{		
		child.transform.position = parent.transform.position;
		child.transform.rotation = parent.transform.rotation;
		child.transform.localScale = parent.transform.localScale;
	}
}
