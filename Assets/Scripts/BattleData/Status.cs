using UnityEngine;
using System.Collections.Generic;


public class Status {
	public string name, desc;
	public string icon;
	public string restriction;
	public bool canDispel;
	public int turns;
	public int removedByDamage, removedByMovement;
	public List<int> effects;
	public string trigger;
	public string condition;
	
	public Status()
	{
		effects = new List<int> ();
		createStatus ("New Status", "SET A DESCRIPTON", "icons/blank", "none","none", false, 
		              0, 0, 0, 0, new List<int>());
	}

	public Status createStatus(string nam, string des, string _icon,
	                           string trig, string cond,
	                           bool canDisp, int tur, 
	                           int turnVa, int rbd, int rbm,
	                           List<int> _effects)
	{
		name = nam;
		desc = des;
		canDispel = canDisp;
		turns = tur;
		condition = cond;
		turns = (int)Random.Range (tur - turnVa, tur + turnVa);
		trigger = trig;
		removedByDamage = rbd;
		removedByMovement = rbm;
		if(effects != null)
			effects = new List<int> (_effects);
		icon = _icon;
		return this;
	}

	public void resolveStatus(BattleActor actor, string trig)
	{
		//Debug.Log("TRIGGER TESTING: " + trig + " - " + trigger);
		string[] conditions = condition.Split(' ');
		foreach (string _condition in conditions) {
			if(_condition.StartsWith("KEYWORD:"))
			{
				if(!ifTargetHasKeyword(_condition.Substring(7), actor))
				{
					return;
				}
			}
		}
		if(trigger.Equals (trig) || trig.Equals("none") && trigger.Equals("once"))
		{
			foreach(int eff in effects)
			{
                Database.Effects().getEffectAtIndex(eff).resolveEffect(actor);
			}
            
		}
		if (trig.Equals ("none") && trigger.Equals ("once")) {
			actor.queueRemoveStatus(this);		
		}
	}
	
	public int passTurn()
	{
		if (turns == 0) {
			return -1;
		}
		return --turns;
	}
	
	public bool getcanDispel()
	{
		return canDispel;
	}

	public bool hasKeyword(string _keyword)
	{
		foreach (int effect in effects) {
			if(Database.Effects().getEffectAtIndex(effect).keynote == _keyword)
			{
				return true;
			}
		}
		return false;
	}
	
	bool ifTargetHasKeyword(string _keyword, BattleActor target)
	{
		foreach (Status statuses in target.getStatusEffects()) {
			foreach(int effect in statuses.effects)
			{
				if(Database.Effects().getEffectAtIndex(effect).keynote == _keyword)
					return true;
			}
		}
		return false;
		
	}
}










