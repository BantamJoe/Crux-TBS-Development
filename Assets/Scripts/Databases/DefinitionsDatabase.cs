using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DefinitionsDatabase {

	//Module name- used for saves etc.
	public string ModuleName = "Default Module";

    //isPrivate exists incase future modules include an online component-
    //Or a module requires a username and password to enter.
    public bool isPrivate = false; 
    
	//TileWidth and height are the height, in UnityStandardUnits, of one tile
	public float tileWidth = 1;
	public float tileHeight = 1;

	//ChunkHeight and Width tell the maximum size of a single 'room'
	//This is not normalized for tileWidth and tileHight
	public int chunkHeight = 16;
	public int chunkWidth = 32;

	//equipmentPositions contains all potential equipment positions
	//Items must contain a position equilivent to one of these to be valid equipment
	public string equipmentPositionsString = "Head Chest Legs Feet LeftArm RightArm Necklace Ring Back";
	public List<string> equipmentPositions;

	//StatTypes add all available Stat Types to the game.
	//Each of these will form in the StatSet as a new stat
	public string statTypesString = "control focus agility power personality perception tethering intelligence fortune";
	public List<string> statTypes;
	public int baseStat = 20;



	public DefinitionsDatabase()
	{		
		equipmentPositions = new List<string>();
		foreach (string equipmentPosition in equipmentPositionsString.Split(' ')) {
			equipmentPositions.Add(equipmentPosition);
		}

		statTypes = new List<string> ();
		foreach (string StatType in statTypesString.Split(' ')) {
			statTypes.Add(StatType);
		}
	}


    public void loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + ModuleName + "/definitionsDatabase.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/definitionsDatabase.dat", FileMode.Open);
            DefinitionsData data = (DefinitionsData)formatter.Deserialize(file);
            ModuleName =  data._ModuleName;
            isPrivate = data._isPrivate;
            tileWidth = data._tileWidth;
            tileHeight = data._tileHeight;
            chunkHeight = data._chunkHeight;
            chunkWidth = data._chunkWidth;
            equipmentPositionsString = data._equipmentPositionsString;
            statTypesString = data._statTypesString;
            baseStat = data._baseStat;
            file.Close();
        }
        foreach (string equipmentPosition in equipmentPositionsString.Split(' '))
        {
            equipmentPositions.Add(equipmentPosition);
        }

        foreach (string StatType in statTypesString.Split(' '))
        {
            statTypes.Add(StatType);
        }
    }

    public void saveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/definitionsDatabase.dat", FileMode.OpenOrCreate);

        DefinitionsData data = new DefinitionsData();

        data._ModuleName = ModuleName;
        data._isPrivate = isPrivate;
        data._tileWidth = tileWidth;
        data._tileHeight = tileHeight;
        data._chunkHeight = chunkHeight;
        data._chunkWidth = chunkWidth;
        data._equipmentPositionsString = equipmentPositionsString;
        data._statTypesString = statTypesString;
        data._baseStat = baseStat;

        formatter.Serialize(file, data);
        file.Close();

    }
}

[Serializable]
class DefinitionsData
{
    public string _ModuleName;
    public bool _isPrivate;
    public float _tileWidth;
    public float _tileHeight;
    public int _chunkHeight;
    public int _chunkWidth;
    public string _equipmentPositionsString;
    public string _statTypesString;
    public int _baseStat;
}