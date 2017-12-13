using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSystem : MonoBehaviour {

	public GameObject healthPrefab;
	public GameObject energyPrefab;
	public GameObject canvas;

	public void loot(Transform location) {
		lootHealth (location);
		lootEnergy (location);
	}

	public void lootHealth(Transform location) {
		if (Random.value < .5) {
			looting (location, healthPrefab, Random.Range (10, 20f));
		}
	}

	public void lootEnergy(Transform location) {
		if (Random.value < .5) {
			looting (location, energyPrefab, Random.Range (10, 20f));
		}
	}

	public void looting(Transform location, GameObject prefab, float value) {
		BubbleLoot instance = Instantiate(prefab.GetComponent<BubbleLoot>());

		Vector2 screenPosition = new Vector2 ();
		screenPosition.Set (location.position.x, location.position.y + 1);



		instance.transform.SetParent (transform, false);
		instance.transform.position = screenPosition;
		instance.setValue(value);
	}

}
