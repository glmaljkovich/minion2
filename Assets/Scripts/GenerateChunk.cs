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
	public float padding;

	// Tiles
	public GameObject stoneTile;
	public GameObject dirtTile;
	public GameObject grassTile;
	public GameObject coalTile;
	public GameObject goldTile;
	public GameObject diamondTile;
	private float lastPosX = 0;

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
			createPerlinTiles (lastPosX);
			lastPosX += (1 + padding);
		}
	}

	private void createPerlinTiles(float x){
		float positionX = x + transform.position.x;
		int perlinHeight = Mathf.RoundToInt (Mathf.PerlinNoise (seed, positionX / smoothness) * heightMultipler) + groundBaseHeight;

		float lastPosY = 0;
		for (int y = 0; y < perlinHeight; y++) {
			createTile (x, lastPosY, y, perlinHeight);
			lastPosY += (1 + padding);
			// TODO: Create background tile
		}
	
	}

	private void createTile (float x, float y, int index, int perlinHeight){
		GameObject selectedTile = getTile (index, perlinHeight);
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
