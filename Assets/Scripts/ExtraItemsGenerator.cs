using UnityEngine;
using System.Collections;

public class ExtraItemsGenerator : MonoBehaviour {
	private PrefabsGenerator[] prefabsGenerators;
	
	public const int SPEED_UP_QUANTITY = 1;
	public const int SLOW_DOWN_QUANTITY = 2;
	public const int JUMP_HIGHER_QUANTITY = 1;
	public const int JUMP_LOWER_QUANTITY = 2;

	void Start () {
		Bounds[] surfacesBounds = GetAllSurfacesBounds ();
		prefabsGenerators = CreatePrefabsGeneratorsFor (surfacesBounds);
	}

	private Bounds[] GetAllSurfacesBounds() {
		GameObject[] surfaces = GameObject.FindGameObjectsWithTag ("Surface");
		Bounds[] result = new Bounds[surfaces.Length];
		
		for (int surfaceNumber = 0 ; surfaceNumber < surfaces.Length ; surfaceNumber++) {
			Collider eachSurfaceCollider = surfaces [surfaceNumber].GetComponent<Collider> ();
			Bounds eachSurfaceBounds = eachSurfaceCollider.bounds;

			result[surfaceNumber] = eachSurfaceBounds;
		}

		return result;
	}

	private PrefabsGenerator[] CreatePrefabsGeneratorsFor(Bounds[] surfacesBounds) {
		PrefabsGenerator[] result = new PrefabsGenerator[surfacesBounds.Length];

		for (int index = 0; index < surfacesBounds.Length; index++) {
			result[index] = new PrefabsGenerator(surfacesBounds[index]);
		}

		return result;
	}

	public void generateBoosts() {
		foreach (PrefabsGenerator eachPrefabsGenerator in prefabsGenerators) {
			eachPrefabsGenerator.generate("SpeedUp", SPEED_UP_QUANTITY);
			eachPrefabsGenerator.generate("SpeedDown", SLOW_DOWN_QUANTITY);
			eachPrefabsGenerator.generate("JumpHigher", JUMP_HIGHER_QUANTITY);
			eachPrefabsGenerator.generate("JumpLower", JUMP_LOWER_QUANTITY);
		}
	}

	public void generateCoins() {
		foreach (PrefabsGenerator eachPrefabsGenerator in prefabsGenerators) {
			eachPrefabsGenerator.generate("Coin");
		}
	}

	// Poniższą metodę można odkomentować do testów
	/*void OnGUI() {
		if (GUILayout.Button ("Coins")) {
			generateCoins();
		}
		if (GUILayout.Button ("Boosts")) {
			generateBoosts();
		}
	}*/
}
