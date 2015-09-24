using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour {
	public UIManager uiManager;
	public Transform inventoryHolder;
	public GameObject inventoryItemButton;

	public void updateInventoryList(List<Item> items)
	{
		clearInventoryHolder ();
		foreach (Item _item in items) {
			GameObject inventoryTab = (GameObject)Instantiate(inventoryItemButton, transform.position, transform.rotation);
			inventoryTab.GetComponent<ItemButton>().setItem( _item);
			inventoryTab.GetComponent<ItemButton>().uiManager = uiManager;
			inventoryTab.GetComponent<ItemButton>().updateIcon();
			inventoryTab.transform.SetParent(inventoryHolder.transform);

		}
	}

	void clearInventoryHolder()
	{
		foreach (Transform child in inventoryHolder.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}
}
