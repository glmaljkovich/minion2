using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour {
	private Player player;
	// Use this for initialization
	void Start () {
		player = gameObject.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		destroyBlock ();
	}

	private void destroyBlock() {
		if (Input.GetMouseButton(0)) {
			
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mousePosition2D = new Vector2 (mousePosition.x, mousePosition.y);

			RaycastHit2D hit = Physics2D.Raycast (mousePosition2D, Vector2.zero);

			GameObject block = (hit.collider != null) ? hit.collider.gameObject : null;

			if (block != null) {
				block.GetComponent<Block> ().mine(player.getToolPower ());
			}
		}
	}
		
}
