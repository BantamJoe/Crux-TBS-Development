using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class BattleActorDatabase : MonoBehaviour {

    public List<BattleActor> battleActors;

    private string filename = "BattleActor";


    public BattleActorDatabase()
    {
        battleActors = new List<BattleActor>();
        loadData();
    }

    public void loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/battleActorDatabase.dat"))
        {
            battleActors.Clear();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/battleActorDatabase.dat", FileMode.Open);
            List<BattleActorData> data = (List<BattleActorData>)formatter.Deserialize(file);
            foreach (BattleActorData _data in data)
            {
                //LOAD DATA
            }
            file.Close();
        }
    }

    public void saveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/battleActorDatabase.dat", FileMode.OpenOrCreate);

        List<BattleActorData> data = new List<BattleActorData>();
        foreach (BattleActor battleActor in battleActors)
        {
            BattleActorData batData = new BattleActorData();

            batData.locationx = battleActor.getMyLocation().x;
            batData.locationy = battleActor.getMyLocation().y;
            batData.locationz = battleActor.getMyLocation().z;
            batData.chunk = battleActor.currentChunk;
            //Model
            //Texture
            batData.passable = battleActor.passable;
            batData.walkable = battleActor.walkable;
            batData.walkableDifficulty = battleActor.walkableDifficulty;
            batData.statSet = battleActor.stats.toString();


    data.Add(batData);
        }

        formatter.Serialize(file, data);
        file.Close();

    }

    public void addBattleActor(BattleActor battleActor)
    {
        battleActors.Add(battleActor);
    }

    public BattleActor createBattleActor( float locationx,
     float locationy,
     float locationz,
     string chunk,
     string model,
     string texture,
     bool passable,
     bool walkable,
     int walkableDifficulty,
     string statSet,
     List<StatusData> statusEffects,
     List<ItemData> inventory,
     List<EquipData> equipment,
     List<SkillData> skillsKnown,
     List<SkillData> skillsEquipped,
     bool blocks)
    {
        //TO DO
        return new BattleActor();
    }

    public BattleActor getBattleActorAtIndex(int index)
    {
        return battleActors.ToArray()[index];
    }

    public int getIndexOfBattleActor(BattleActor battleActor)
    {
        return battleActors.IndexOf(battleActor);
    }

    public int GetDataCount()
    {
        return battleActors.Count;
    }

    public void RemoveData(int index)
    {
        battleActors.RemoveAt(index);
    }
}

[Serializable]
public class BattleActorData
{
    public float locationx;
    public float locationy;
    public float locationz;
    public int chunk;
    public string model;
    public string texture;
    public bool passable;
    public bool walkable;
    public int walkableDifficulty;
    public string statSet;
    public List<StatusData> statusEffects;
    public List<ItemData> inventory;
    public List<EquipData> equipment;
    public List<int> skillsKnown;
    public List<int> skillsEquipped;
    public bool blocks;
}