using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
	public Text inventoryText;
	public Text selectedBlockText;
	private BlockType selectedBlock;
	private Dictionary <BlockType, int> inventory;
	private InventoryUI inventoryUI;
	private BlockList blocklist;

	// Use this for initialization
	void Start ()
	{
		inventory = new Dictionary<BlockType, int> ();
		inventoryUI = FindObjectOfType<InventoryUI> () as InventoryUI;
		blocklist = FindObjectOfType<BlockList> () as BlockList;
	}

	// Update is called once per frame
	void Update ()
	{
		updateSelectedBlockText ();
	}

	private void updateSelectedBlockText(){
		if (selectedBlock != null) {
			selectedBlockText.text = "<b>" + selectedBlock + "</b>";
		}
	}

	public void AddBlocks(Block origin, int count){
		BlockType type = origin.type;

		if (!inventory.ContainsKey (type))
			inventory [type] = 0;

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

	public GameObject getSelectedBlock(){
		return blocklist.getOriginalBlock(selectedBlock);
	}

	public void setSelectedBlock(BlockType block) {
		this.selectedBlock = block;
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
}

