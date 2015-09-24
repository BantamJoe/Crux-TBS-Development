using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Skill 
{
	public string name, desc;
	string skillType;
	string icon;
	List<string> costs; //FORMAT:   "stat:cost"
	int successRate;
	
	BattleActor caster, target;

	int range;

	Status effect;
	List<string> requirements;

	public Skill()
	{
		costs = new List<string> ();
		requirements = new List<string> ();
	}

	public void createSkill(string _name, string _desc, string _skillType, string _icon, 
	                        List<string> _costs, int _successRate, int _range, 
	                        Status _effect, List<string> _requirements)
	{
		name = _name;
		desc = _desc;
		skillType = _skillType;
		icon = _icon;
		costs = _costs;
		successRate = _successRate;
		range = _range;
		effect = _effect;
		requirements = _requirements;
	}

	public void createSkill(string _name, string _desc, string _skillType, string _icon, 
		            List<string> _costs, int _successRate, int _range, 
		            int _effect, List<string> _requirements)
	{
		createSkill (_name, _desc, _skillType, _icon, 
		            _costs, _successRate, _range, 
		            Database.Statuses ().getStatusAtIndex (_effect), _requirements);
	}

	public void addEffect(Status _effect)
	{
		effect = _effect;
	}

	public bool canCast(BattleActor actor)
	{
		foreach (string requirement in requirements) {
			if (requirement.StartsWith ("weapon:")) {
				if (!actor.hasEquipmentType (requirement.Substring (7, requirement.Length - 7))) {
					return false;
				}
			}
			if (requirement.StartsWith ("keyword:")) {
				if (!actor.hasKeyword (requirement.Substring (8, requirement.Length - 8))) {
					return false;
				}
			}
			if (requirement.StartsWith ("stat:")) {
				if (actor.getStat (requirement.Split (':') [1]) < int.Parse (requirement.Split (':') [2])) {
					return false;
				}
			}

		
		}
		return true;
	}

	public void useSkill(BattleActor caster, BattleActor target)
	{
		foreach (string cost in costs) {
			caster.modifyStat(cost.Split(':')[0], int.Parse(cost.Split(':')[1]));
		}
		if (Random.Range (0, 100) > successRate) {
			return; //Skill failed. 
		}

		target.addStatus (effect);
	}

    public Sprite getIcon()
    {
        return (Sprite)Resources.Load(icon);
    }

}


















