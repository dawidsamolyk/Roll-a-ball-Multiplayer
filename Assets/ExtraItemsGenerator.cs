using UnityEngine;
using System.Collections;

public class ExtraItemsGenerator : MonoBehaviour {
	private PrefabsGenerator[] prefabsGenerators;

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
			eachPrefabsGenerator.generate("SpeedUp", 1);
			eachPrefabsGenerator.generate("SpeedDown", 2);
			eachPrefabsGenerator.generate("JumpHigher", 1);
			eachPrefabsGenerator.generate("JumpLower", 2);
		}
	}

	public void generateCoins() {
		foreach (PrefabsGenerator eachPrefabsGenerator in prefabsGenerators) {
			eachPrefabsGenerator.generate("Coin");
		}
	}

	// Poniższą metodę można odkomentować do testów
	void OnGUI() {
		if (GUILayout.Button ("Coins")) {
			generateCoins();
		}
		if (GUILayout.Button ("Boosts")) {
			generateBoosts();
		}
	}
}
