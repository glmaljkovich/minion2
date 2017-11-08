using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {
	public Sprite sprite;
	public Block item;
	public int quantity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public BlockType getBlockType(){
		return item.type;
	}
}
