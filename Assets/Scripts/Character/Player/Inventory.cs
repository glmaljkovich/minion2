using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	public Text inventoryText;
	public Text selectedBlockText;
	public GameObject[] blocks;
	private int selectedBlockIndex;
	private Dictionary <BlockType, int> inventory;
	// Use this for initialization
	void Start ()
	{
		inventory = new Dictionary<BlockType, int> ();

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
		selectedBlockText.text = "Selected: <b>" + blockType + "</b>";
	}

	public void AddBlocks(BlockType type, int count){
		inventory [type] += count;
	}

	public void removeBlocks(BlockType type, int count) {
		inventory [type] += count;
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
}

