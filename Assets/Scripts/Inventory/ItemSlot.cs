using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
	public Image image;
	public Item item;
	public Text counterText;
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

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right && item != null) {
			print("Set selected block");
			inventory.setSelectedBlock (item.getBlockType ());
		}
	}
}

