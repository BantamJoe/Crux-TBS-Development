using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class CharacterPanel : MonoBehaviour {

	public Text name;
	public ItemButton Head, LArm, RArm, Chest,
		Leg, Feet, Necklace, Ring, Back; 
	public List<Button> skillButtons;
	public Text stats;
	public Slider healthBar, manaBar, staminaBar;

	public Sprite noEquipIcon;

	public BattleActor actor;

	public void createPanel(BattleActor _actor)
	{
		actor = _actor;
		updateCharacterPanel ();
	}
	public void updateCharacterPanel()
	{
		name.text = actor.name;
		updateSkillButtons ();
		updateStatsList ();
		updateStatusBars ();
		updateEquipmentList ();
	}

	void updateStatusBars()
	{
		healthBar.maxValue = actor.getStat ("maxHealth");
		healthBar.value = actor.getStat ("currentHealth");
		manaBar.maxValue = actor.getStat ("maxMana");
		manaBar.value = actor.getStat ("currentMana");
		staminaBar.maxValue = actor.getStat ("maxStamina");
		staminaBar.value = actor.getStat ("currentStamina");
	}

	void updateSkillButtons()
	{
		int i = 0;
		
		List<Skill> equippedSkillsList = actor.getEquippedSkills ();
		
		foreach (Button _button in skillButtons) {
			_button.gameObject.SetActive(false);	
		}
		
		foreach (Skill equippedSkills in equippedSkillsList) {
			//			Debug.Log(skillButtons.ToArray().Length);
			
			skillButtons.ToArray()[i].image.sprite = equippedSkills.getIcon();
			skillButtons.ToArray()[i].gameObject.SetActive(true);	
			i++;
		}
	}

	void updateStatsList()
	{

		string _stats = "";
		_stats += "Control: ";
		_stats += actor.getStat ("control");
		_stats += "\nFocus: ";
		_stats += actor.getStat("focus");
		_stats += "\nAgility: ";
		_stats += actor.getStat("agility");
		_stats += "\nPower: ";
		_stats += actor.getStat("power");
		_stats += "\nPersonality: ";
		_stats += actor.getStat("personality");
		_stats += "\nPerception: ";
		_stats += actor.getStat("perception");
		_stats += "\nTethering: ";
		_stats += actor.getStat("tethering");
		_stats += "\nIntelligence: ";
		_stats += actor.getStat("intelligence");
		_stats += "\nFortune: ";
		_stats += actor.getStat("fortune");
		stats.text = _stats;
	}

	void updateEquipmentList()
	{
		Head.disable ();
		LArm.disable ();
		RArm.disable ();
		Chest.disable ();
		Leg.disable ();
		Feet.disable ();
		Necklace.disable ();
		Ring.disable ();
		Back.disable ();
		//public Button Head, LArm, RArm, Chest,
		//Leg, Feet, Necklace, Ring, Back;
		foreach (Equipment equip in actor.getEquipment()) {
			//Debug.Log(equip.name + " BEING ADDED");
			if(equip.getEquipmentPosition().Equals("Head"))
			{
				//Debug.Log ("HAT");
				Head.setItem(equip);
			}
			if(equip.getEquipmentPosition().Equals("Necklace"))
			{
				Necklace.setItem(equip);
			}
			if(equip.getEquipmentPosition().Equals("LArm"))
			{
				LArm.setItem(equip);
			}
			if(equip.getEquipmentPosition().Equals("RArm"))
			{
				RArm.setItem(equip);
			}
			if(equip.getEquipmentPosition().Equals("Chest"))
			{
				Chest.setItem(equip);
			}
			if(equip.getEquipmentPosition().Equals("Leg"))
			{
				Leg.setItem(equip);
			}
			if(equip.getEquipmentPosition().Equals("Feet"))
			{
				Feet.setItem(equip);
			}
			if(equip.getEquipmentPosition().Equals("Ring"))
			{
				Ring.setItem(equip);
			}
			if(equip.getEquipmentPosition().Equals("Back"))
			{
				Back.setItem(equip);
			}
		}
		Head.updateIcon();
		Necklace.updateIcon();
		LArm.updateIcon();
		RArm.updateIcon();
		Chest.updateIcon();
		Leg.updateIcon();
		Feet.updateIcon();
		Ring.updateIcon();
		Back.updateIcon();


	}
}





























