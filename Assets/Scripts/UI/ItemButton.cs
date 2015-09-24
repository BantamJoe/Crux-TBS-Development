using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour {
	public Item item;
	public Button thisButton;
	public UIManager uiManager;
	public bool active;
	public Sprite emptySprite;

	public ItemButton()
	{
		active = false;
	}

	public void setItem(Item _item)
	{
		item = _item;
		active = true;
	}

	public void disable()
	{
		active = false;
	}

	public void updateIcon()
	{
		if (!active) {
			thisButton.image.sprite = emptySprite;
			return;
		}
        thisButton.image.sprite = item.getIcon();
	}

	public void onClick()
	{
		if(active)
		uiManager.displayItemPanel (item);
	}
}
