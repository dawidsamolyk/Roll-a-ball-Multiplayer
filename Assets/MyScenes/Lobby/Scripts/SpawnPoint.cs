using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public Transform[] spawnPoints;

	public Vector3 GetSpawnPosition(int playerID)
	{
		int spawnNumber = playerID % spawnPoints.Length;
		return spawnPoints[spawnNumber].position;
	}

}
