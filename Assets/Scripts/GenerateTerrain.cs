using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {
	public GameObject chunk;
	int chunkWidth;
	public int numChunks;
	public GameObject player;
	private PlayerMovementInX playerDirection;
	float seed;
	float seed2;
	float playerLastPositiveX;
	float playerLastNegativeX;
	float lastX;
	float lastNegativeX;
	public float chunkWidthMultiplier = 2f;

	// Use this for initialization
	void Start () {
		chunkWidth = chunk.GetComponent<GenerateChunk> ().width;
		lastX = -chunkWidth;
		lastNegativeX = -chunkWidth;
		seed = Random.Range (-10000f, 10000f);
		seed2 = Random.Range (-10000f, 10000f);
		Generate ();
		playerLastPositiveX = player.transform.position.x;
		playerLastNegativeX = player.transform.position.x;
		playerDirection = player.GetComponent<PlayerMovementInX> ();
	}
	
	// Update is called once per frame
	public void Generate () {
		for (int i = 0; i < numChunks; i++) {
			GameObject newChunk = Instantiate (chunk, new Vector3 (lastX + chunkWidth, 0f), Quaternion.identity) as GameObject;
			newChunk.GetComponent<GenerateChunk> ().seed = seed;
			lastX += chunkWidth*chunkWidthMultiplier;
		}
	}

	public void GenerateUpdated(){
		for (int i = 0; i < numChunks; i++) {
			GameObject newChunk = Instantiate (chunk, new Vector3 (lastX + chunkWidth, 0f), Quaternion.identity) as GameObject;
			newChunk.GetComponent<GenerateChunk> ().seed = seed;
			lastX += chunkWidth*chunkWidthMultiplier;
		}
	}

	public void GenerateNegativeUpdated(){
		for (int i = 0; i < numChunks; i++) {
			GameObject newChunk = Instantiate (chunk, new Vector3 (lastNegativeX - chunkWidth, 0f), Quaternion.identity) as GameObject;
			newChunk.GetComponent<GenerateChunk> ().seed = seed2;
			lastNegativeX -= chunkWidth*chunkWidthMultiplier;
		}
	}

	void Update() {
		// By default the terrain renders 2 chunks, so this way we check when we are half way of the current terrain.
		generatePositiveXChunks();
		generateNegativeXChunks ();
	}

	void generatePositiveXChunks() {
		if (playerDirection.getIsRight() &&	Mathf.RoundToInt(player.transform.position.x) == Mathf.RoundToInt(playerLastPositiveX + chunkWidth)) {
			playerLastPositiveX += chunkWidthMultiplier * chunkWidth; 
			GenerateUpdated ();
		}	
	}

	void generateNegativeXChunks() {
		if (!playerDirection.getIsRight() && Mathf.RoundToInt(player.transform.position.x) == Mathf.RoundToInt(playerLastNegativeX - chunkWidth)) {
			playerLastNegativeX -= chunkWidthMultiplier * chunkWidth; 
			GenerateNegativeUpdated ();
		}	
	}
}
