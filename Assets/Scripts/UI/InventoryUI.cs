using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
	public const int numItemSlots = 12;

//	public Image[] itemImages = new Image[numItemSlots];
//	public Item[] items = new Item[numItemSlots];
//	public Text[] itemCounters = new Text[numItemSlots];
	public ItemSlot[] slots = new ItemSlot[numItemSlots]; 

	public void AddItem(Item itemToAdd)
	{
		if (hasItem (itemToAdd)) {
			updateItemCount (itemToAdd, itemToAdd.quantity);
			// types was already modified by caller with new item count. Leave.
			return;
		}

		int i = Array.FindIndex (slots, slot => slot.item == null);
		ItemSlot emptySlot = slots[i];
		emptySlot.item = itemToAdd;
		emptySlot.image.sprite = itemToAdd.sprite;
		emptySlot.image.enabled = true;
		emptySlot.counterText.text = itemToAdd.quantity.ToString();
		emptySlot.counterText.enabled = true;
//		int i = Array.FindIndex (items, item => item == null);
//		items[i] = itemToAdd;
//		itemImages[i].sprite = itemToAdd.sprite;
//		itemImages[i].enabled = true;
//		itemCounters [i].text = itemToAdd.quantity.ToString();
//		itemCounters[i].enabled = true;
	}

	void increaseItemCount(Item item) {
		item.quantity += 1;
		updateItemCount (item, item.quantity);
	}

	void decreaseItemCount(Item item) {
		updateItemCount (item, item.quantity);
	}

	void updateItemCount(Item itemToUpdate, int count){
//		int i = Array.FindIndex (items, item => item.getBlockType().Equals(itemToUpdate.getBlockType()));
		int i = Array.FindIndex (slots, slot => slot.item.getBlockType().Equals(itemToUpdate.getBlockType()));
//		itemCounters [i].text = count.ToString();
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
//				items[i] = null;
//				itemImages[i].sprite = null;
//				itemImages[i].enabled = false;
//				itemCounters [i].text = "0";
//				itemCounters[i].enabled = false;
				return;
			}
		}
	}
}

