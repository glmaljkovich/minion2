using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChunk : MonoBehaviour {

	// Variables
	public int width;
	public float heightMultipler;
	public int tileDelta;
	public float smoothness;
	public int groundBaseHeight;
	[HideInInspector]
	public float seed;

	// Tiles
	public GameObject stoneTile;
	public GameObject dirtTile;
	public GameObject grassTile;
	public GameObject coalTile;
	public GameObject goldTile;
	public GameObject diamondTile;

	// Chances
	[Range (0, 100)]
	public float coalChance;
	[Range (0, 100)]
	public float goldChance;
	[Range (0, 100)]
	public float diamondChance;

	void Start () {
		Generate ();
	}

	public void Generate () {
		// Columns iteration
		for (int i = 0; i < width; i++) {
			// Creates a column
			createPerlinTiles (i);
		}
	}

	private void createPerlinTiles(int x){
		float positionX = x + transform.position.x;
		int perlinHeight = Mathf.RoundToInt (Mathf.PerlinNoise (seed, positionX / smoothness) * heightMultipler) + groundBaseHeight;

		for (int y = 0; y < perlinHeight; y++) {
			createTile (x, y, perlinHeight);
			// TODO: Create background tile
		}
	
	}

	private void createTile (int x, int y, int perlinHeight){
		GameObject selectedTile = getTile (y, perlinHeight);
		GameObject newTile = Instantiate (selectedTile, Vector3.zero, Quaternion.identity) as GameObject;

		newTile.transform.parent = this.gameObject.transform;
		newTile.transform.localPosition = new Vector3 (x, y);

	}

	private GameObject getTile(int y, int height) {
		GameObject selectedTile;

		if (y < height - tileDelta) {
			selectedTile = getMineral();
		} else if (y < height - 1) {
			selectedTile = dirtTile;
		} else {
			selectedTile = grassTile;
		}

		return selectedTile;
	
	}

	private GameObject getMineral() {
		float randomChance = Random.Range (0, 100);
		GameObject selectedMineral;

		if (randomChance < diamondChance) {
			selectedMineral = diamondTile;
		} else if (randomChance < goldChance) {
			selectedMineral = goldTile;
		} else if (randomChance < coalChance) {
			selectedMineral = coalTile;
		} else {
			selectedMineral = stoneTile;
		}

		return selectedMineral;
	}
		
}
