using UnityEngine;
using System.Collections.Generic;

/*************************************************
    The Equipment class holds all data for any item that can be equipped.
    Note that equipment must have an EQUIPMENTPOSITION equal to one of the
    positions defined in the DefinitionsDatabase, or it will be unuseable.

    */
public class Equipment : Item {

	public string equipmentType;
	public string equipmentPosition;

	public List<Status> effects;

	public List<string> requirements;

	public Equipment()
	{
		effects = new List<Status> ();
		requirements = new List<string> ();
	}

	public string getEquipmentPosition()
	{
		return equipmentPosition;
	}

	public string getEquipmentType()
	{
		return equipmentType;
	}

	public void addEffect(Status effect)
	{
		effects.Add (effect);
	}

	public bool checkRequirements(BattleActor player)
	{
		foreach (string requirement in requirements) {
			if(requirement.StartsWith("keyword:"))
			{
				if(!player.hasKeyword(requirement.Substring(8, requirement.Length-8)))
				{
					return false;
				}
			}
			if(requirement.StartsWith("stat:"))
			{
				if(player.getStat(requirement.Split(':')[1]) < int.Parse(requirement.Split (':')[2]))
				{
					return false;
				}
			}
		}

		return true;
	}

	public void useItem(BattleActor target)
	{
        if (target.isEquipped(this))
        {
            target.removeEquipment(this);
            return;
        }
        if (canUse(target))
        {
            target.addEquipment(this);
        }
        
    }


    public void resolveStatus(BattleActor actor)
    {
        Debug.Log("RESOLVE STATUS: EQUIP" + effects.Count);
        foreach (Status stateff in effects)
        {
            stateff.resolveStatus(actor, "Equipment");
        }
    }


    public string getItemType()
	{
		return "equipment";
	}

}











































