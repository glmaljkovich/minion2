using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
	public Image[] itemImages = new Image[numItemSlots];
	public Item[] items = new Item[numItemSlots];
	public Text[] itemCounters = new Text[numItemSlots];

	public const int numItemSlots = 6;

	public void AddItem(Item itemToAdd)
	{
		if (hasItem (itemToAdd)) {
			updateItemCount (itemToAdd, itemToAdd.quantity);
			// types was already modified by caller with new item count. Leave.
			return;
		}

		int i = Array.FindIndex (items, item => item == null);
		items[i] = itemToAdd;
		itemImages[i].sprite = itemToAdd.sprite;
		itemImages[i].enabled = true;
		itemCounters [i].text = itemToAdd.quantity.ToString();
		itemCounters[i].enabled = true;
	}

	void increaseItemCount(Item item) {
		item.quantity += 1;
		updateItemCount (item, item.quantity);
	}

	void decreaseItemCount(Item item) {
		updateItemCount (item, item.quantity);
	}

	void updateItemCount(Item itemToUpdate, int count){
		int i = Array.FindIndex (items, item => item.getBlockType().Equals(itemToUpdate.getBlockType()));

		itemCounters [i].text = count.ToString();
	}

	bool hasItem(Item anItem){
		return items[0] != null && Array.Exists (items, item => item != null &&  item.getBlockType ().Equals (anItem.getBlockType ()));
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
		for (int i = 0; i < items.Length; i++)
		{
			if (items[i] == itemToRemove)
			{
				items[i] = null;
				itemImages[i].sprite = null;
				itemImages[i].enabled = false;
				itemCounters [i].text = "0";
				itemCounters[i].enabled = false;
				return;
			}
		}
	}
}

