using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class StatusDatabase : BaseDatabase{
	
	public List<Status> statuses;
	
	private string filename = "status";
	
	
	public StatusDatabase()
	{
		statuses = new List<Status> ();
		loadData ();
	}
	
	public void loadData()
	{
		if (File.Exists (Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/statusDatabase.dat")) {
			statuses.Clear();
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/statusDatabase.dat", FileMode.Open);
			List<StatusData> data = (List<StatusData>)formatter.Deserialize(file);
			foreach(StatusData _data in data)
			{
				Status staData = new Status();
				staData.name = _data._name;
				staData.desc = _data._desc;
				staData.icon = _data._icon;
				staData.restriction = _data._restriction;
				staData.canDispel = _data._canDispel;
				staData.turns = _data._turns;
				staData.removedByDamage = _data._removedByDamage;
				staData.removedByMovement = _data._removedByMovement;
				foreach(int effData in _data._effects)
				{
					staData.effects.Add(effData);
				}
				staData.trigger = _data._trigger;
				statuses.Add(staData);
			}
			file.Close();
		}
	}
	
	public void saveData()
	{
		BinaryFormatter formatter = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/statusDatabase.dat", FileMode.OpenOrCreate);
		List<StatusData> staDataHolder = new List<StatusData> ();
		foreach (Status status in statuses)
		{
			StatusData staData = new StatusData();
			staData._effects = new List<int>();
			staData._name = status.name;
			staData._desc = status.desc;
			staData._icon = status.icon;
			staData._restriction = status.restriction;
			staData._canDispel = status.canDispel;
			staData._turns = status.turns;
			staData._removedByDamage = status.removedByDamage;
			staData._removedByMovement = status.removedByMovement;
			foreach(int effect in status.effects)
			{
					staData._effects.Add(effect);
			}
			
			staData._trigger = status.trigger;
			staDataHolder.Add(staData);
		}
		
		formatter.Serialize (file, staDataHolder);
		file.Close ();
		
	}
	
	public void addStatus(Status status)
	{
		statuses.Add(status);
	}
	
	public Status getStatusAtIndex(int index )
	{
		return statuses.ToArray () [index];
	}
	
	public int GetDataCount()
	{
		return statuses.Count;
	}
	
	
}

[Serializable]
class StatusData
{
	public string _name;
	public string _desc;
	public string _icon;
	public string _restriction;
	public bool _canDispel;
	public int _turns;
	public int _removedByDamage;
	public int _removedByMovement;
	public List<int> _effects;
	public string _trigger;
}