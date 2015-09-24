using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatSet  {
	//baseStat[0] and stat[0] match up to statTypes[0]
	public List<string> statTypes;
	public int[] baseStat;
	public int[] stat;
	

	public StatSet()
	{
        statTypes = new List<string>();
		//baseStat (Health) = Max Health.
		//Stat(health) = current.
		statTypes.Add ("Health");
		statTypes.Add ("Mana");
		statTypes.Add ("Stamina");
		foreach (string statType in Database.Definitions().statTypes) {
			statTypes.Add(statType);
		}

		baseStat = new int[statTypes.Count];
		stat = new int[statTypes.Count];
		for (int i = 0; i < statTypes.Count; i++) {
			baseStat[i] = Database.Definitions().baseStat;
			stat[i] = Database.Definitions().baseStat;
		}
		calculateVitalStats ();
		regenerateAllStats ();
	}

	public void calculateVitalStats()
	{
		//0 = Health, 1 = Mana, 2 = Stamina
		//Calculate Health
		int tempMax = baseStat [0];
		baseStat [0] = Database.Formula ().statSetParse (this, Database.Formula ().maxHealthFormula);
		if (tempMax != 0) {
			stat[0] = (int) ((float)stat[0] / tempMax * baseStat[0]);
		}
		//Calculate Mana
		tempMax = baseStat [1];
		baseStat [1] = Database.Formula ().statSetParse (this, Database.Formula ().maxManaFormula);
		if (tempMax != 0) {
			stat[1] = (int) ((float)stat[1] / tempMax * baseStat[1]);
		}
		//Calculate Stamina
		tempMax = baseStat [0];
		baseStat [2] = Database.Formula ().statSetParse (this, Database.Formula ().maxStaminaFormula);
		if (tempMax != 0) {
			stat[2] = (int) ((float)stat[2] / tempMax * baseStat[2]);
		}



	}

	void regenerateAllStats()
	{
		stat [0] = baseStat [0]; //Fill Health
		stat [1] = baseStat [1]; //Fill Mana
		stat [2] = baseStat [2]; //Fill Stamina
	}

	public int getMaxJump()
	{
		return Database.Formula ().statSetParse (this, Database.Formula ().maxJumpFormula);
	}

	public int getMoveSpeed()
	{
		return Database.Formula ().statSetParse (this, Database.Formula ().moveSpeedFormula);
	}

	public int getStat(string _stat)
	{
        if (_stat == "currentHealth")
            return stat[0];
        else if (_stat == "maxHealth")
            return baseStat[0];
        else if (_stat == "currentMana")
            return stat[1];
        else if (_stat == "maxMana")
            return baseStat[1];
        else if (_stat == "currentStamina")
            return stat[2];
        else if (_stat == "maxStamina")
            return baseStat[2];
        return stat[getIndexof(_stat)];
	}

	public int getBaseStat(string _stat)
	{
		return baseStat [getIndexof(_stat)];
	}

	public void resetStats()
	{
		for(int i = 0; i < statTypes.Count; i++)
		{
			stat[i] = baseStat[i];
		}
	}

	public void modifyStat(string _stat, int modifyBy)
	{
		stat[getIndexof(_stat)] += modifyBy;
	}

	public void modifyBaseStat(string _stat, int modifyBy)
	{
		baseStat[getIndexof(_stat)] += modifyBy;
	}

	int getIndexof(string _stat)
	{

		for (int i = 0; i < statTypes.Count; i++)
			if (statTypes.ToArray () [i].Equals (_stat))
				return i;
		Debug.LogError ("ERROR: STAT CALLED, NOT FOUND. STAT: " + _stat);
		return 0;
	}

    public string toString()
    {
        string returnable = "";
        for (int i = 0 ; i < statTypes.Count ; i++)
        {
            returnable += statTypes.ToArray()[i] + ",";
            returnable += baseStat[i] + ",";
            returnable += stat[i];
            returnable += ":";
        }
        return returnable;
    }

    public void loadStats(string statString)
    {
        int i = 0;
        foreach (string statParse in statString.Split(':'))
        {
            string[] parsed = statParse.Split(',');
            stat[i] = int.Parse(parsed[1]);
            baseStat[i] = int.Parse(parsed[2]);
            i++;
        }

    }



}

