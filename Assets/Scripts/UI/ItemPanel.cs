using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour {
	public UIManager uiManager;
	public Button useButton, moveButton;
	public Text name, desc, statDesc, canUse;
	public Image image;

	Item item;
	
	void Start()
	{
		item = new Item();
		item.name = "NOTHING";
	}

	public void setItem(Item _item)
	{
		item = _item;
	}

	public void updateItemPanel()
	{
		if (item.name.Equals ("NOTHING"))
						return;
		name.text = item.name;
		desc.text = item.desc;
		image.sprite = item.getIcon();
		updateCanUse ();
		updateButtons ();
		updateStatDesc ();
	}
	public void updateStatDesc()
	{
		string _statDesc = "";

		statDesc.text = _statDesc;
	}

	public void updateCanUse()
	{
		if (item.canUse (uiManager.getPlayerBattleActor ())) {
						canUse.text = "CAN USE";
				} else {
						canUse.text = "CANNOT USE";
			useButton.gameObject.SetActive(false);
				}
	}

	public void updateButtons()
	{
		Item act = item;
		if (canUse.text.Equals ("CANNOT USE")) {
			useButton.gameObject.SetActive (false);
		} else {
			useButton.gameObject.SetActive(true);	
		}
		if (item.getItemType ().Equals ("Equipment")) {
						if (uiManager.getPlayerBattleActor ().isEquipped ((Equipment)item)) {
								useButton.transform.GetComponentInChildren<Text> ().text = "Unequip";
						} else {
								useButton.transform.GetComponentInChildren<Text> ().text = "Equip";
						}
		} else 
		{
			useButton.transform.GetComponentInChildren<Text>().text = "Use";
		}



	}

	public void useItem()
	{	

		if (item.getItemType ().Equals ("Equipment")) {
						Equipment testEquip;
						testEquip = item as Equipment;
						testEquip.useItem (uiManager.getPlayerBattleActor ());
				}
		else
			item.useItem (uiManager.getPlayerBattleActor());
		uiManager.updateUI ();
		
	}
}
















