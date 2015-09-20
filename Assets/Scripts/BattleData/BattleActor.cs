using UnityEngine;
using System.Collections.Generic;

public class BattleActor : MonoBehaviour {


	//Basic GameObject Information
	public GameObject navPoint;


	//Battle Stats
	StatSet stats;

	//Status Effect tracker
	List<Status> statusEffects;
	List<Status> removeStatusQueue;

	//Equipment and Inventory management
	public List<Item> inventory;
	public List<Equipment> equipment;
	public List<Skill> skillsKnown;
	public List<Skill> skillsEquipped;

	//Battle Functionality Data
	public string team;
	public bool blocks;
	public bool hasTurn;


	public BattleActor()
	{
		statusEffects = new List<Status> ();
		removeStatusQueue = new List<Status> ();
		equipment = new List<Equipment> ();
		skillsKnown = new List<Skill> ();
		skillsEquipped = new List<Skill> ();
		inventory = new List<Item> ();

	}
	
	public int getStat(string _stat)
	{
		return stats.getStat (_stat);
	}

	public void modifyStat(string _stat, int _amount)
	{
		stats.modifyStat (_stat, _amount);
	}

	public List<Status> getStatusEffects()
	{
		return statusEffects;
	}

	public void queueRemoveStatus(Status removeStat)
	{
		removeStatusQueue.Add (removeStat);
	}

	public void purgeRemoveStatusQueue()
	{
		foreach (Status removeStat in removeStatusQueue) {
			statusEffects.Remove(removeStat);
		}
		removeStatusQueue.Clear ();
	}

	void updateStats()
	{
		updateStats ("none");
	}

	void updateStats(string trigger)
	{
		stats.resetStats ();
		//EQUIPMENT

		foreach (Status stateff in statusEffects) {
			stateff.resolveStatus(this, trigger);
		}
		if (removeStatusQueue.Count > 0) {
			purgeRemoveStatusQueue();
		}

		stats.calculateVitalStats ();
	}

	public List<Equipment> getEquipment()
	{
		return equipment;
	}

	public void fullDispel()
	{
		foreach (Status stateff in statusEffects) {
			if(stateff.getcanDispel())
			{
				removeStatusQueue.Add(stateff);
			}
			if (removeStatusQueue.Count > 0) {
				purgeRemoveStatusQueue();
			}
			updateStats();
		}
	}

	public void startTurn()
	{
		//Add our Mana per Turn to current Mana, making sure we don't go beyond the max.
		stats.modifyStat("Mana", Database.Formula().statSetParse(stats, 
		                                                         Database.Formula().perTurnManaRegenFormula));
		if (stats.getStat ("Mana") > stats.getBaseStat ("Mana")) {
			stats.modifyStat("Mana", stats.getStat ("Mana") - stats.getBaseStat ("Mana"));
		}

		//Fill Stamina
		stats.modifyStat ("Stamina", stats.getBaseStat ("Stamina") + stats.getStat ("Stamina"));
	}

	public void endTurn()
	{
		foreach (Status stat in statusEffects) {
			if(stat.passTurn() == 0)
			{
				removeStatusQueue.Add(stat);
			}
		}
		if (removeStatusQueue.Count > 0) {
			purgeRemoveStatusQueue();
		}
		updateStats ();
	}

	public void calculateMovement(int movement)
	{
		stats.modifyBaseStat ("Stamina", -movement);
	}

	public void addStatus(Status statEff)
	{
		statusEffects.Add (statEff);
		updateStats ();
	}

	public void removeStatus(Status _status)
	{
		statusEffects.Remove(_status);
		updateStats ();
	}

	public bool getHasTurn()
	{
		return hasTurn;
	}

	public bool hasKeyword(string keyword)
	{
		foreach (Status stat in statusEffects) {
			if(hasKeyword(keyword))
				return true;
		}
		return false;
	}

	public bool isEquipped(Equipment equip)
	{
		return equipment.Contains (equip);
	}

	public void addEquipment(Equipment equip)
	{
		if (equip.canUse (this)) {
			removeEquipment (equip.getEquipmentPosition ());
			equipment.Add (equip);
			updateStats ();
		}
	}

	public void removeEquipment(Equipment equip)
	{
		equipment.Remove (equip);
		updateStats ();
	}

	public void removeEquipment(string slot)
	{
		foreach (Equipment equip in equipment) {
			if(equip.getEquipmentPosition().Equals(equip))
			{
				removeEquipment(equip);
			}
		}
	}

	public void checkEquipmentRequirements()
	{
		foreach (Equipment equip in equipment) {
			if(!equip.canUse(this))
			{
				removeEquipment(equip);
				checkEquipmentRequirements();
			}
		}
	}

	public bool hasEquipmentType(string type)
	{
		foreach (Equipment equip in equipment) {
			if(equip.getEquipmentType().Equals(type))
				return true;
		}
		return false;
	}

	public void addItem(int itemNumber)
	{
		//DOESN'T EXIST YET!
	}

	public void addItem(Item item)
	{
		inventory.Add (item);
	}

	public void removeItem(Item item)
	{
		inventory.Remove (item);
	}


	public void addSkill(Skill skill)
	{
		skillsKnown.Add (skill);
	}

	public void removeSkill(Skill skill)
	{
		skillsEquipped.Remove (skill);
		skillsKnown.Remove (skill);
	}
	public void equipSkill(Skill skill)
	{
		skillsEquipped.Add (skill);
	}
	public void unequipSkill(Skill skill)
	{
		skillsEquipped.Remove (skill);
	}

	public List<Skill> getEquippedSkills()
	{
		return skillsEquipped;
	}

	void checkDeath()
	{
		if (stats.getStat ("Health") <= 0) {
			hasTurn = false;
			stats.modifyBaseStat("Health", -stats.getStat("Health"));
		}
	}
}
























