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
		createBlock ();
	}

	private void destroyBlock() {
		if (Input.GetMouseButton(0)) {
			
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mousePosition2D = new Vector2 (mousePosition.x, mousePosition.y);

			RaycastHit2D hit = Physics2D.Raycast (mousePosition2D, Vector2.zero);

			GameObject block = (hit.collider != null) ? hit.collider.gameObject : null;

			if (block != null) {
				block.GetComponent<Block> ().mine (player.getToolPower ());
			}
		}
	}

	private void createBlock(){
		if (Input.GetMouseButton(1)) {

			Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 mousePosition2D = new Vector2 (mousePosition.x, mousePosition.y);

			RaycastHit2D hit = Physics2D.Raycast (mousePosition2D, Vector2.zero);

			GameObject block = (hit.collider != null) ? hit.collider.gameObject : null;

			if (block == null) {
				Vector3 position = new Vector3 (Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y), 0);

				GameObject originTile = player.inventory.getSelectedBlock ();

				Block origin = originTile.GetComponent<Block> ();

				GameObject newTile = Instantiate (player.inventory.getSelectedBlock (), position, Quaternion.identity) as GameObject;

				newTile.transform.localScale = originTile.transform.localScale;
				newTile.GetComponent<SpriteRenderer>().sortingLayerName = "terrain";
				newTile.GetComponent<Block> ().hardness = origin.hardness;
			}
		}
	}
		
}
