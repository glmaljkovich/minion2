using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour {
	private Player player;
	private Inventory inventory;

	// Use this for initialization
	void Start () {
		player = gameObject.GetComponent<Player> ();
		inventory = player.inventory;
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
				placeBlock (position);
			}
		}
	}

	private void placeBlock(Vector3 position) {
		if (inventory.canAddSelectedBlock ()) {
			GameObject originTile = inventory.getSelectedBlock ();

			inventory.removeBlocks (originTile.GetComponent<Block>(), 1);

			instantiateBlock (originTile, position);

		}
	}

	private void instantiateBlock(GameObject originTile, Vector3 position){
		GameObject newTile = Instantiate (originTile, position, Quaternion.identity) as GameObject;

		newTile.transform.localScale = originTile.transform.localScale;
		newTile.GetComponent<SpriteRenderer>().sortingLayerName = "terrain";
		newTile.GetComponent<Block> ().hardness = originTile.GetComponent<Block>().hardness;
	
	}
		
}
