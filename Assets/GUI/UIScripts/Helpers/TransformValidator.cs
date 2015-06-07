using UnityEngine;
using System.Collections;

public class TransformValidator {

	/**
	 * Reset item transformation to avoid complication with items size, position etc.
	 */
	public static void ResetItemTransform(GameObject item)
	{
		item.transform.position = new Vector3 (0f, 0f, 0f);
		item.transform.rotation = new Quaternion (0f, 0f, 0f, 0f);
		item.transform.localScale = new Vector3 (1f, 1f, 1f);
	}
}
