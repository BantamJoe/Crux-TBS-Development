using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class Item : MonoBehaviour {

	public string name, desc;
	public string icon;
	public string itemType;
	public int price;
	public bool consumable, consumed;
	public bool useableOutsideBattle;
	public string scope; //(Target, All Allies, All Enemies, Self)
	//DAMAGE FORMULA
	public List<Effect> useEffects;
	
	public Item()
	{
		name = "DEFAULT NAME";
		desc = "DEFAULT DESC";
		useEffects = new List<Effect> ();
	}

	public void createItem(string _name, string _desc, string _icon, string _itemType, int _price, 
	                       bool _consumable, bool _consumed, bool _useableOutsideBattle, 
	                       string _scope, List<Effect> _useEffects)
	{
		name = _name;
		desc = _desc;
		icon = _icon;
		itemType = _itemType;
		price = _price;
		consumable = _consumable;
		consumed = _consumed;
		useableOutsideBattle = _useableOutsideBattle;
		scope = _scope;
		useEffects = _useEffects;
	}

	public void createItem(string _name, string _desc, string _icon, string _itemType, int _price, 
	                       bool _consumable, bool _consumed, bool _useableOutsideBattle, 
	                       string _scope, List<int> _useEffects)
	{
		List<Effect> itemUseEffects = new List<Effect> ();
		foreach (int addEffect in _useEffects) {
			itemUseEffects.Add(Database.Effects().getEffectAtIndex(addEffect));
		}
		createItem (_name, _desc, _icon, _itemType, _price, 
		            _consumable, _consumed, _useableOutsideBattle, 
		           _scope, itemUseEffects);
	}

	public void useItem(BattleActor target)
	{
		foreach (Effect effect in useEffects) {
			effect.resolveEffect(target);
		}
	}

	public string getItemType()
	{
		return itemType;
	}

	public bool canUse(BattleActor actor)
	{
		return true;
	}

    public Sprite getIcon()
    {
        return  (Sprite)Resources.Load(icon, typeof(Sprite));

    }

    
}



















