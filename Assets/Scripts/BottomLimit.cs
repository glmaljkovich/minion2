using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomLimit : MonoBehaviour {
	public Player player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			player.Die();
		}
	}

}
