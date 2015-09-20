using UnityEngine;
using System.Collections.Generic;

public class Equipment : Item {

	string equipmentType;
	public string equipmentPosition;

	List<Status> effects;

	List<string> requirements;

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
		
	}

	public string getItemType()
	{
		return "equipment";
	}

}











































