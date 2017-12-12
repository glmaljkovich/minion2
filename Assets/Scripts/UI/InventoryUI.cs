using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
	public const int numItemSlots = 12;
	public ItemSlot[] slots = new ItemSlot[numItemSlots]; 

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
		return Array.Exists (slots, slot => slot.item != null &&  slot.item.getBlockType ().Equals (anItem.getBlockType ()));
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
			if (slots[i].item.getBlockType() == itemToRemove.getBlockType())
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

