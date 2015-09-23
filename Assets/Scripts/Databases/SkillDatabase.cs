using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SkillDatabase : BaseDatabase
{

    public List<Skill> skills;

    private string filename = "skills";


    public SkillDatabase()
    {
        skills = new List<Skill>();
        loadData();
    }

    public void loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/skillDatabase.dat"))
        {
            skills.Clear();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/skillDatabase.dat", FileMode.Open);
            List<SkillData> data = (List<SkillData>)formatter.Deserialize(file);
            foreach (SkillData _data in data)
            {
               //LOAD DATA
            }
            file.Close();
        }
    }

    public void saveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/skillDatabase.dat", FileMode.OpenOrCreate);

        List<SkillData> data = new List<SkillData>();
        foreach (Skill skill in skills)
        {
            SkillData skiData = new SkillData();
            
            data.Add(skiData);
        }

        formatter.Serialize(file, data);
        file.Close();

    }

    public void addSkill(Skill skill)
    {
        skills.Add(skill);
    }

    public Skill createSkill(string _name,
     string _desc,
     string _icon,
     List<string> _costs,
     int _successRate,
     int _range,
     StatusData _effect,
     List<string> _requirements)
    {
    //TO DO
        return new Skill();
    }

    public Skill getSkillAtIndex(int index)
    {
        return skills.ToArray()[index];
    }

    public int getIndexOfSkill(Skill skill)
    {
        return skills.IndexOf(skill);
    }

    public int GetDataCount()
    {
        return skills.Count;
    }

    public void RemoveData(int index)
    {
        skills.RemoveAt(index);
    }


}


[Serializable]
public class SkillData
{
    public string _name;
    public string _desc;
    public string _icon;
    public List<string> _costs;
    public int _successRate;
    public int _range;
    public StatusData _effect;
    public List<string> _requirements;
}