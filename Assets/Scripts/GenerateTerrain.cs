using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {
	public GameObject chunk;
	int chunkWidth;
	public int numChunks;
	public GameObject player;
	float seed;
	float playerLastX;
	float lastX;

	// Use this for initialization
	void Start () {
		chunkWidth = chunk.GetComponent<GenerateChunk> ().width;
		seed = Random.Range (-10000f, 10000f);
		Generate ();
		playerLastX = player.transform.position.x;
	}
	
	// Update is called once per frame
	public void Generate () {
		lastX = -chunkWidth;

		for (int i = 0; i < numChunks; i++) {
			GameObject newChunk = Instantiate (chunk, new Vector3 (lastX + chunkWidth, 0f), Quaternion.identity) as GameObject;
			newChunk.GetComponent<GenerateChunk> ().seed = seed;
			lastX += chunkWidth*2;
		}
	}

	public void GenerateUpdated(){
		for (int i = 0; i < numChunks; i++) {
			GameObject newChunk = Instantiate (chunk, new Vector3 (lastX + chunkWidth, 0f), Quaternion.identity) as GameObject;
			newChunk.GetComponent<GenerateChunk> ().seed = seed;
			lastX += chunkWidth*2;
		}
	}

	void Update() {
		if (Mathf.RoundToInt(player.transform.position.x) == Mathf.RoundToInt(playerLastX + chunkWidth)) {
			playerLastX += 2 * chunkWidth; 
			GenerateUpdated ();
		}	
	}
}
