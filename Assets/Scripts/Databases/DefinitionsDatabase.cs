using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefinitionsDatabase : MonoBehaviour {

	//Module name- used for saves etc.
	public string ModuleName = "Default Module";

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
}
