using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ItemDatabase : BaseDatabase
{

    public List<Item> items;

    private string filename = "Item";


    public ItemDatabase()
    {
        items = new List<Item>();
        loadData();
    }

    public void loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/itemDatabase.dat"))
        {
            items.Clear();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/itemDatabase.dat", FileMode.Open);
            List<ItemData> data = (List<ItemData>)formatter.Deserialize(file);
            foreach (ItemData _data in data)
            {
                Item item = new Item();
                item.name = _data._name;
                item.desc = _data._desc;
                item.icon = _data._icon;
                item.price = _data._price;
                item.consumable = _data._consumable;
                item.consumed = _data._consumed;
                item.useableOutsideBattle = _data._useableOutsideBattle;
                item.scope = _data._scope;
                foreach (EffectData effect in _data._useEffects)
                {
                   
                    item.useEffects.Add(Database.Effects().createEffect(effect._name, effect._addStatus, effect._keyNote, effect._modifyStat,
                         effect._modifyStatFormula, effect._valueLocked));
                }
                items.Add(item);
            }
            file.Close();
        }
    }

    public void saveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/itemDatabase.dat", FileMode.OpenOrCreate);

        List<ItemData> data = new List<ItemData>();
        foreach (Item item in items)
        {
            ItemData iteData = new ItemData();
            iteData._name = item.name;
            iteData._desc = item.desc;
            iteData._icon = item.icon;
            iteData._price = item.price;
            iteData._consumable = item.consumable;
            iteData._consumed = item.consumed;
            iteData._useableOutsideBattle = item.useableOutsideBattle;
            iteData._scope = item.scope;
            foreach (Effect effect in item.useEffects)
            {
                EffectData effData = new EffectData();
                effData._name = effect.name;
                effData._keyNote = effect.keynote;
                effData._addStatus = effect.addStatis;
                effData._modifyStat = effect.modifyStat;
                effData._modifyStatFormula = effect.modifyStatFormula;
                effData._valueLocked = effect.valueLocked;
                iteData._useEffects.Add(effData);
            }
            
         data.Add(iteData);
        }

        formatter.Serialize(file, data);
        file.Close();

    }

    public void addItem(Item item)
    {
        items.Add(item);
    }

    public Item createItem(string _name,
     string _desc,
     string _icon,
     int _price,
     bool _consumable, bool _consumed,
     bool _useableOutsideBattle,
     string _scope,
     List<EffectData> _useEffects)
    {
        //TO DO
        return new Item();
    }

    public Item getItemAtIndex(int index)
    {
        return items.ToArray()[index];
    }

    public int getIndexOfItem(Item item)
    {
        return items.IndexOf(item);
    }

    public int GetDataCount()
    {
        return items.Count;
    }

    public void RemoveData(int index)
    {
        items.RemoveAt(index);
    }


}


[Serializable]
public class ItemData
{
    public string _name;
    public string _desc;
    public string _icon;
    public int _price;
    public bool _consumable, _consumed;
    public bool _useableOutsideBattle;
    public string _scope;
    public List<EffectData> _useEffects;
}