using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
	public Text inventoryText;
	public Text selectedBlockText;
	public GameObject[] blocks;
	private int selectedBlockIndex;
	private Dictionary <BlockType, int> inventory;
	private InventoryUI inventoryUI;

	// Use this for initialization
	void Start ()
	{
		inventory = new Dictionary<BlockType, int> ();
		inventoryUI = FindObjectOfType<InventoryUI> () as InventoryUI;

		foreach (BlockType aType in System.Enum.GetValues(typeof(BlockType))) {
			inventory.Add (aType, 0);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		inventoryText.text = getReadableBlockCount ();

		if(selectedBlockIndex < 0){
			selectedBlockIndex = blocks.Length - 1;
		} else if (selectedBlockIndex > blocks.Length - 1) {
			selectedBlockIndex = 0;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			selectedBlockIndex++;
			updateSelectedBlockText ();
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			selectedBlockIndex--;
			updateSelectedBlockText ();
		}
			

	}

	private void updateSelectedBlockText(){
		string blockType = blocks [selectedBlockIndex].GetComponent<Block> ().type.ToString ();
		selectedBlockText.text = "<b>" + blockType + "</b>";
	}

	public void AddBlocks(Block origin, int count){
		BlockType type = origin.type;

		inventory [type] += count;

		Item item = createItem (origin, inventory [type]);

		inventoryUI.AddItem (item);

	}

	public void removeBlocks(Block origin, int count) {
		BlockType type = origin.type;

		if (inventory [type] > 0) {
			inventory [type] -= count;
			// Poniendo la tapa y volviendola a sacar? no.
			Item item = createItem (origin, inventory [type]);

			inventoryUI.RemoveItem (item);
		}

	}

	public int getBlockCount(BlockType type) {
		return inventory [type];
	}

	public string getReadableBlockCount(){
		string result = "Inventory - ";

		foreach(KeyValuePair<BlockType, int> entry in inventory)
		{
			string blockCount = entry.Key.ToString () + ": \t" + entry.Value.ToString () + ": \t";
			result += blockCount;
		}

		return result;
	}

	public GameObject getSelectedBlock(){
		return blocks [selectedBlockIndex];
	}

	public bool canAddSelectedBlock() {
		BlockType type = getSelectedBlock().GetComponent<Block>().type; 
		return inventory [type] > 0;
	}

	Item createItem(Block origin, int count) {
		Item item = Item.CreateInstance <Item>();

		item.sprite = origin.image;
		item.item = origin;
		item.quantity = count;

		return item;
	}

	public GameObject getOriginalBlock(BlockType type) {
		return Array.Find(blocks, gObject => gObject.GetComponent<Block>().type.Equals(type));
	}
}

