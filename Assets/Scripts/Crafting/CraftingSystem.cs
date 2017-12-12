using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CraftingSystem : MonoBehaviour
{
	public const int numItemSlots = 9;
	public ItemSlot[] slots = new ItemSlot[numItemSlots]; 
	public Recipe[] recipes;
	public ItemSlot recipeSlot;
	private Recipe currentRecipe;
	private Inventory inventory;
	// Use this for initialization
	void Start ()
	{
		inventory = FindObjectOfType<Inventory> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	// TODO:
	// - instance block game objects from a generic origin and build them passing the block
	// - Make prefabs for new blocks

	void OnItemPlace(DragAndDropCell.DropDescriptor desc)
	{
		DummyControlUnit sourceSheet = desc.sourceCell.GetComponentInParent<DummyControlUnit>();
		DummyControlUnit destinationSheet = desc.destinationCell.GetComponentInParent<DummyControlUnit>();
		// If item dropped between different sheets
		if (destinationSheet != sourceSheet)
		{
			Debug.Log(desc.item.name + " is dropped from " + sourceSheet.name + " to " + destinationSheet.name);
		}

		findRecipes ();
	}

	public void findRecipes(){
		foreach (Recipe recipe in recipes) {
			if (recipe.matches(getItems())) {
				currentRecipe = recipe;
				showRecipe (recipe);
				return;
			}
		}
	}

	public List<Item> getItems() {
		List<Item> items = new List<Item> ();
		foreach (ItemSlot slot in slots) {
			items.Add (slot.item);
		}
		return items;
	}

	public void showRecipe(Recipe recipe){
		Item item = recipe.result;
		recipeSlot.item = item;
		recipeSlot.image.sprite = item.sprite;
		recipeSlot.image.enabled = true;
		recipeSlot.counterText.text = item.quantity.ToString();
		recipeSlot.counterText.enabled = true;
	}

	public void craft(){
		if (currentRecipe != null) {
			foreach (Item ingredient in currentRecipe.ingredients) {
				Item matchingItem = getItems ().Find (item => item.getBlockType ().Equals (ingredient.getBlockType ()));
				matchingItem.quantity -= ingredient.quantity;
				inventory.removeBlocks (ingredient.item, ingredient.quantity);
			}
			inventory.AddBlocks (currentRecipe.result.item, currentRecipe.result.quantity);
			clearSlots ();
		}
	}

	public void clearSlots (){
	}
}

