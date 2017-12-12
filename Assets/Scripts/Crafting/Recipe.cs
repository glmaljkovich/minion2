using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewRecipe", menuName = "Inventory/Recipe", order = 1)]
public class Recipe : ScriptableObject {
	public Item[] ingredients;
	public Item result;
	public string name;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public bool matches (List<Item> items){
		bool result = true;
		foreach (Item ingredient in ingredients) {
			result = result && items.Exists(item => item.getBlockType ().Equals (ingredient.getBlockType ()) && item.quantity >= ingredient.quantity);
		}
		return result;
	}
}
