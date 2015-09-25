using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class EffectDatabase : BaseDatabase{
	
	public List<Effect> effects;
	
	private string filename = "effects";
	
	
	public EffectDatabase()
	{
		effects = new List<Effect> ();
		loadData ();
	}
	
	public void loadData()
	{
		if (File.Exists (Application.persistentDataPath + "/" + Database.Definitions().ModuleName +  "/effectDatabase.dat")) {
			effects.Clear();
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath +  "/" + Database.Definitions().ModuleName + "/effectDatabase.dat", FileMode.Open);
			List<EffectData> data = (List<EffectData>)formatter.Deserialize(file);
			foreach(EffectData _data in data)
			{
				effects.Add(createEffect(_data._name, _data._addStatus, _data._keyNote, _data._modifyStat, 
				                         _data._modifyStatFormula, _data._valueLocked));
            }
			//			effects = data.effects;
			file.Close();
		}
	}
	
	public void saveData()
	{
		BinaryFormatter formatter = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath +  "/" + Database.Definitions().ModuleName + "/effectDatabase.dat", FileMode.OpenOrCreate);
		
		List<EffectData> data = new List<EffectData> ();
		foreach (Effect effect in effects)
		{
			EffectData effData = new EffectData();
			effData._name = effect.name;
			effData._keyNote = effect.keynote;
			effData._addStatus = effect.addStatis;
			effData._modifyStat = effect.modifyStat;
			effData._modifyStatFormula = effect.modifyStatFormula;
			effData._valueLocked = effect.valueLocked;
			data.Add(effData);
		}
		
		formatter.Serialize (file, data);
		file.Close ();
		
	}
	
	public void addEffect(Effect effect)
	{
		effects.Add (effect);
	}
	
	public Effect createEffect(string _name, int _addStatus, 
	                           string _keyNote, string _modifyStat, 
	                           string _modifyStatFormula, bool _valueLocked 
	                           )
	{
		return new Effect (_name, _addStatus, _keyNote,
		                   _modifyStat, _modifyStatFormula,
		                   _valueLocked);
	}
	
	public Effect getEffectAtIndex(int index )
	{
		return effects.ToArray () [index];
	}

	public int getIndexOfEffect(Effect effect)
	{
		int i = 0;
		foreach (Effect eff in effects) {
			if(effect.name == eff.name && effect.modifyStatFormula == eff.modifyStatFormula)
				return i;
			i++;
		}
		Debug.Log ("EFFECT INDEX NOT FOUND! EFFECT: " + effect.name);
		return -1;
	}

	public int GetDataCount()
	{
		return effects.Count;
	}
	
	public void RemoveData(int index)
	{
		effects.RemoveAt (index);
	}
	
	
}


[Serializable]
public class EffectData
{
	public string _name;
	public int _addStatus;
	public string _keyNote;
	public string _modifyStat;
	public string _modifyStatFormula; 
	public bool _valueLocked; 
	
}