using UnityEngine;
using System.Collections;

public class PrefabsGenerator {
	public const float EXTRA_PREFABS_HEIGHT = 10;
	private float minX, minZ, maxX, maxZ, maxY;
	private int prefabsQuantity;
	
	public PrefabsGenerator (Bounds planeBounds)
	{
		if (planeBounds == null) {
			throw new MissingReferenceException ("Nieustawiony obszar, na którym mają być generowane obiekty!!");
		}
		
		Vector3 min = planeBounds.min;
		Vector3 max = planeBounds.max;
		
		minX = min.x;
		minZ = min.z;
		maxX = max.x;
		maxZ = max.z;
		maxY = max.y;

		prefabsQuantity = (int) planeBounds.size.magnitude / 10;

		if (prefabsQuantity < 1) {
			prefabsQuantity = 1;
		}
	}

	public GameObject[] generate (string prefabName, int quantity)
	{
		if (prefabName == null || prefabName.Length == 0) {
			throw new MissingReferenceException ("Nieustawiona nazwa obiektu, który ma być wygenerowany!");
		}
		
		GameObject[] result = new GameObject[quantity];
		
		for (int i = 0; i < quantity; i++) {
			Vector3 position = getRandomPositionOnPlaneWithExtraHeight();
			GameObject prefab = PhotonNetwork.Instantiate (prefabName, position, Quaternion.identity, 0);
			
			result[i] = prefab;
		}
		
		return result;
	}
	
	public GameObject[] generate (string prefabName)
	{
		return generate (prefabName, prefabsQuantity);
	}
	
	public Vector3 getRandomPositionOnPlaneWithExtraHeight() {
		float x = Random.Range (minX, maxX);
		float z = Random.Range (minZ, maxZ);
		
		return new Vector3 (x, maxY + EXTRA_PREFABS_HEIGHT, z);
	}
}
