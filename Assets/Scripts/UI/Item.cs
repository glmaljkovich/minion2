using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item", order = 1)]
public class Item : ScriptableObject {
	public Sprite sprite;
	public Block item;
	public int quantity;

	public BlockType getBlockType(){
		return item.type;
	}
}
