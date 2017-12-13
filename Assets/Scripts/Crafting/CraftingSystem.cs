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
	public Text materials;
	public Text recipeName;
	private Recipe currentRecipe;
	private Inventory inventory;
	private InventoryUI inventoryUI;
	// Use this for initialization
	void Start ()
	{
		inventory = FindObjectOfType<Inventory> ();
		inventoryUI = FindObjectOfType<InventoryUI> ();
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
			if (slot.item != null) {
				items.Add (slot.item);
			}
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
		recipeName.text = recipe.name;
		showIngredients (recipe);
	}

	public void showIngredients(Recipe recipe){
		string result = "";
		foreach (Item item in recipe.ingredients) {
			result += item.quantity.ToString() + " " + item.getBlockType().ToString() + "\n";
		}
		materials.text = result;
	}

	public void craft(){
		if (currentRecipe != null) {
			consumeIngredients ();
			returnRemainingBlocks ();
			inventory.AddBlocks (currentRecipe.result.item, currentRecipe.result.quantity);
			clearSlots ();
			clearRecipe ();
		}
	}

	void consumeIngredients() {
		foreach (Item ingredient in currentRecipe.ingredients) {
			Item matchingItem = getItems ().Find (item => item.getBlockType ().Equals (ingredient.getBlockType ()));
			matchingItem.quantity -= ingredient.quantity;
			inventory.removeBlocks (ingredient.item, ingredient.quantity);
		}
	}

	void returnRemainingBlocks() {
		foreach (Item item in getItems()) {
			inventoryUI.AddItem(item);
		}
	}

	void clearSlot(ItemSlot slot){
		slot.item = null;
		slot.image.sprite = null;
		slot.image.enabled = false;
		slot.counterText.text = "0";
		slot.counterText.enabled = false;
	}

	void clearRecipe(){
		clearSlot (recipeSlot);
		recipeName.text = "";
		materials.text = "";
	}

	public void clearSlots (){
		ItemSlot[] itemSlots = Array.FindAll (slots, slot => slot.item != null);
		foreach (ItemSlot slot in itemSlots) {
			clearSlot (slot);
		}
	}
}

