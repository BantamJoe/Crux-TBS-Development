using UnityEngine;
using System.Collections;


public class Effect {

	//VARIABLES

	public string name;
	BattleActor caster, target; //BattleActors are only used in value locked formulas.
	public int addStatis, removeStatus; //The int is the DATABASE location of the status to add or remove
	public string keynote; //KEYNOTES are information tidbits used for, among other things, conditionals
	public string modifyStat; //Which stat to modify
	public string modifyStatFormula; //The formula for how to modify the stat.
	public bool valueLocked; 
	int lockedValue;
	
	public Effect()
	{
		createEffect ("Effect Name", -1, "", "", "", false);
	}
	
	public Effect(string _name, int _addStatus, 
	              string _keyNote, string _modifyStat, 
	              string _modifyStatFormula, bool _valueLocked )
	{
		createEffect (_name, _addStatus, _keyNote, _modifyStat, _modifyStatFormula, _valueLocked);
	}
	
	public void createEffect(string _name, int _addStatus, 
	                         string _keyNote, string _modifyStat, 
	                         string _modifyStatFormula, bool _valueLocked 
	                         )
	{
		name = _name;
		addStatis = _addStatus;
		keynote = _keyNote;
		modifyStat = _modifyStat;
		modifyStatFormula = _modifyStatFormula;
		valueLocked = _valueLocked;
	}
	
	public void setAffectStat(string stat, string formula)
	{
		modifyStat = stat;
		modifyStatFormula = formula;
	}
	
	public void setAffectStat(string stat, string formula, bool locked, BattleActor caster, BattleActor target)
	{
		valueLocked = locked;
		if (valueLocked) {
			lockedValue = Database.Formula().damageParse(caster, target, formula);
		}
		modifyStat = stat;
		modifyStatFormula = formula;
		
	}
	
	public void affectStat(string stat, string formula, bool locked, BattleActor caster, BattleActor target)
	{
		if (valueLocked) {
			target.modifyStat (stat, lockedValue);
		}
		else{
			lockedValue = Database.Formula().damageParse (caster, target, formula);
		}
		valueLocked = locked;
		target.modifyStat (stat, lockedValue);
		
	}
	
	public void resolveEffect (BattleActor caster)
	{
		resolveEffect (caster, caster);
	}
	
	public void resolveEffect(BattleActor caster, BattleActor target)
	{

		if (!modifyStatFormula.Equals ("")) {
            Debug.Log("MODIFYING STAT " + modifyStat + " WITH FORMULA " + modifyStatFormula);
			target.modifyStat(modifyStat, Database.Formula().damageParse(caster, target, modifyStatFormula));
		}
		
	}
}



//Triggers: Nine, on hit, on attack, on casting, on moving, per turn, after x turns, after x actions, on action, on action anyone, instant
//public int control, focus, agility, power, personality, perception,tethering, intelligence, fortune;