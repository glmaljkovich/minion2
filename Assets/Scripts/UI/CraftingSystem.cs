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
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	// TODO:
	// - Global blocklist separate from selectable blocks to use.
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

	public void AddItem(Item itemToAdd)
	{
		if (hasItem (itemToAdd)) {
			updateItemCount (itemToAdd, itemToAdd.quantity);
			return;
		}

		int i = Array.FindIndex (slots, slot => slot.item == null);
		ItemSlot emptySlot = slots[i];
		emptySlot.item = itemToAdd;
		emptySlot.image.sprite = itemToAdd.sprite;
		emptySlot.image.enabled = true;
		emptySlot.counterText.text = itemToAdd.quantity.ToString();
		emptySlot.counterText.enabled = true;
	}

	void increaseItemCount(Item item) {
		item.quantity += 1;
		updateItemCount (item, item.quantity);
	}

	void decreaseItemCount(Item item) {
		updateItemCount (item, item.quantity);
	}

	void updateItemCount(Item itemToUpdate, int count){
		int i = Array.FindIndex (slots, slot => slot.item.getBlockType().Equals(itemToUpdate.getBlockType()));
		slots[i].counterText.text = count.ToString();
	}

	bool hasItem(Item anItem){
		return slots[0].item != null && Array.Exists (slots, slot => slot.item != null &&  slot.item.getBlockType ().Equals (anItem.getBlockType ()));
	}

	public void RemoveItem (Item itemToRemove)
	{
		if (hasItem (itemToRemove)) {
			if (itemToRemove.quantity == 0) {
				removeItemFromList (itemToRemove);
			} else {
				updateItemCount (itemToRemove, itemToRemove.quantity);
			}

		}

	}

	private void removeItemFromList(Item itemToRemove){
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i].item == itemToRemove)
			{
				ItemSlot slot = slots [i];
				slot.item = null;
				slot.image.sprite = null;
				slot.image.enabled = false;
				slot.counterText.text = "0";
				slot.counterText.enabled = false;
				return;
			}
		}
	}
}

