using UnityEngine;
using System.Collections;

public class GenerateCoins : MonoBehaviour
{
	private float minX, minZ, maxX, maxZ, maxY;
	public GameObject coinPrefab;

	void Start ()
	{
		Bounds planeBounds = GetComponent<Collider>().bounds;
		Vector3 min = planeBounds.min;
		Vector3 max = planeBounds.max;

		minX = min.x;
		minZ = min.z;
		maxX = max.x;
		maxZ = max.z;
		maxY = max.y;
	}

	public void generateCoins(int quantity) 
	{
		if (coinPrefab == null) {
			throw new MissingReferenceException("Nieustawiony prefab monety (Coin)!");
		}

		for (int i = 0; i < quantity; i++) {
			float x = Random.Range (minX, maxX);
			float z = Random.Range (minZ, maxZ);
			Vector3 position = new Vector3 (x, maxY + 5, z);
			
			GameObject.Instantiate (coinPrefab, position, Quaternion.identity);
		}
	}

	void OnGUI ()
	{
		if (GUILayout.Button ("Generate coins")) {
			generateCoins(10);
		}
	}
}
