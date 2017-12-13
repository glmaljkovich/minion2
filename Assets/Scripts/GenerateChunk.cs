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
	public float foodY;

	// Tiles
	public GameObject stoneTile;
	public GameObject dirtTile;
	public GameObject grassTile;
	public GameObject coalTile;
	public GameObject ironTile;
	public GameObject goldTile;
	public GameObject diamondTile;

	// Food
	public GameObject food;

	private float lastPosX = 0;

	// Chances
	[Range (0, 100)]
	public float coalChance;
	[Range (0, 100)]
	public float ironChance;
	[Range (0, 100)]
	public float goldChance;
	[Range (0, 100)]
	public float diamondChance;
	[Range (0, 100)]
	public float foodChance;

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
		GameObject selectedTile = getTile (index, perlinHeight, x, y);
		GameObject newTile = Instantiate (selectedTile, Vector3.zero, Quaternion.identity) as GameObject;

		newTile.transform.parent = this.gameObject.transform;
		newTile.transform.localPosition = new Vector3 (x, y);

	}

	private GameObject getTile(int y, int height, float theX, float theY) {
		GameObject selectedTile;

		if (y < height - tileDelta) {
			selectedTile = getMineral();
		} else if (y < height - 1) {
			selectedTile = dirtTile;
		} else {
			putFood (theX, theY);
			selectedTile = grassTile;
		}

		return selectedTile;
	
	}

	private void putFood(float theX, float theY){
		float randomChance = Random.Range (0, 100);
		if(randomChance <= foodChance) {
			GameObject foodInstance = Instantiate (food, Vector3.zero, Quaternion.identity) as GameObject;

			foodInstance.transform.parent = this.gameObject.transform;
			foodInstance.transform.localPosition = new Vector3 (theX, theY + foodY);
		}
	}

	private GameObject getMineral() {
		float randomChance = Random.Range (0, 100);
		GameObject selectedMineral;

		if (randomChance < diamondChance) {
			selectedMineral = diamondTile;
		} else if (randomChance < goldChance) {
			selectedMineral = goldTile;
		} else if (randomChance < ironChance) {
			selectedMineral = ironTile;
		} else if (randomChance < coalChance) {
			selectedMineral = coalTile;
		} else {
			selectedMineral = stoneTile;
		}

		return selectedMineral;
	}
		
}
