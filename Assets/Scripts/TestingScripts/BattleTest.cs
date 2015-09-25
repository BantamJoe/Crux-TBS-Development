using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/*************************************************
    This is purely a class for testing information related to the battle mechanics.
    All work here should be considered scrap.
    */

public class BattleTest : MonoBehaviour {
    public BattleActor one, two;
    public Chunk chunk;


	// Use this for initialization
	public void Start () {
        Database.loadDatabase();
        Player player = new Player();
        player.setUsername("MY USERNAME");
        player.setBattleActor(one);

        Database.GameManager().setPlayer(player);

        Database.GameManager().currentChunk =
            chunk;

        Equipment addEquipment = new Equipment();
        addEquipment.equipmentType = "Heavy Armor";
        addEquipment.equipmentPosition = "Head";
        Effect newEff = new Effect();
        newEff.createEffect("Equipment Effect", -1, "", "agility", "100", false);
        Database.Effects().addEffect(newEff);
        
        List<int> effs = new List<int>();
        effs.Add(4);
        addEquipment.effects.Add(new Status().createStatus("Well Equipped", "Your Equipment gives your strength", "Icons/2", "Equipment","",false, -1, 0, 0, 0, effs ));
        addEquipment.requirements = new List<string>();
        List<Effect> effList = new List<Effect>();
        effList.Add(newEff);
        addEquipment.createItem("MY EQUIPMENT", "SOME EQUIP", "Icons/4", "Equipment", 100, true, false, true, "", effList);
        one.addItem(addEquipment);

        Status newStat = new Status();
        effs.Clear();
        effs.Add(0);
        newStat.createStatus("BURN", "BURNINATER", "Icons/12", "none", "", true, 3, 0, 0, 0, effs);
        Skill newSkill = new Skill();
        Database.Statuses().addStatus(newStat);
        List<string> costs = new List<string>();
        costs.Add("currentMana:12");
        newSkill.createSkill("Burn", "Burninate Enemies", "", "Icons/12", costs, 100, 3, 0, new List<string>());
        one.addSkill(newSkill);
        one.equipSkill(newSkill);

}
	
}
