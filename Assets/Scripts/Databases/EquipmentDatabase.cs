using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class EquipmentDatabase : BaseDatabase
{

    public List<Equipment> equips;

    private string filename = "Equips";


    public EquipmentDatabase()
    {
        equips = new List<Equipment>();
        loadData();
    }

    public void loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/equipmentDatabase.dat"))
        {
            equips.Clear();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/equipmentDatabase.dat", FileMode.Open);
            List<EquipData> data = (List<EquipData>)formatter.Deserialize(file);
            foreach (EquipData _data in data)
            {
                Equipment equip = new Equipment();
                equip.name = _data._name;
                equip.desc = _data._desc;
                equip.icon = _data._icon;
                equip.price = _data._price;
                equip.consumable = _data._consumable;
                equip.consumed = _data._consumed;
                equip.useableOutsideBattle = _data._useableOutsideBattle;
                equip.scope = _data._scope;
                foreach (EffectData effect in _data._useEffects)
                {

                    equip.useEffects.Add(Database.Effects().createEffect(effect._name, effect._addStatus, effect._keyNote, effect._modifyStat,
                         effect._modifyStatFormula, effect._valueLocked));
                }

                equip.equipmentPosition = _data._equipmentPosition;
                equip.equipmentType = _data._equipmentType;

                foreach (string requirement in _data._requirements)
                {
                    equip.requirements.Add(requirement);
                }

                foreach (StatusData status in _data._effects)
                {
                    Status staData = new Status();
                    staData.name = status._name;
                    staData.desc = status._desc;
                    staData.icon = status._icon;
                    staData.restriction = status._restriction;
                    staData.canDispel = status._canDispel;
                    staData.turns = status._turns;
                    staData.removedByDamage = status._removedByDamage;
                    staData.removedByMovement = status._removedByMovement;
                    foreach (int effData in status._effects)
                    {
                        staData.effects.Add(effData);
                    }
                    staData.trigger = status._trigger;
                    equip.effects.Add(staData);
                }

                equips.Add(equip);
            }
            file.Close();
        }
    }

    public void saveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/equipmentDatabase.dat", FileMode.OpenOrCreate);

        List<EquipData> data = new List<EquipData>();
        foreach (Equipment equip in equips)
        {
            EquipData equData = new EquipData();
            equData._requirements = new List<string>();
            equData._useEffects = new List<EffectData>();
            equData._effects = new List<StatusData>();
            equData._name = equip.name;
            equData._desc = equip.desc;
            equData._icon = equip.icon;
            equData._price = equip.price;
            equData._consumable = equip.consumable;
            equData._consumed = equip.consumed;
            equData._useableOutsideBattle = equip.useableOutsideBattle;
            equData._scope = equip.scope;
            foreach (Effect effect in equip.useEffects)
            {
                EffectData effData = new EffectData();
                effData._name = effect.name;
                effData._keyNote = effect.keynote;
                effData._addStatus = effect.addStatis;
                effData._modifyStat = effect.modifyStat;
                effData._modifyStatFormula = effect.modifyStatFormula;
                effData._valueLocked = effect.valueLocked;
                equData._useEffects.Add(effData);
            }
            equData._equipmentType = equip.getEquipmentType();
            equData._equipmentPosition = equip.getEquipmentPosition();

            foreach (string requirement in equip.requirements)
            {
                equData._requirements.Add(requirement);
            }

            foreach (Status status in equip.effects)
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
                foreach (int effect in status.effects)
                {
                    staData._effects.Add(effect);
                }

                staData._trigger = status.trigger;
                equData._effects.Add(staData);
            }

            data.Add(equData);
        }

        formatter.Serialize(file, data);
        file.Close();

    }

    public void addEquipment(Equipment equip)
    {
        equips.Add(equip);
    }

    public Equipment createEquipment(string _name,
     string _desc,
     string _icon,
     int _price,
     bool _consumable, bool _consumed,
     bool _useableOutsideBattle,
     string _scope,
     List<EffectData> _useEffects,
     string equipmentType,
     string equipmentPosition,
    List<StatusData> effects,
    List<string> requirements)
    {
        //TO DO
        return new Equipment();
    }

    public Equipment getEquipmentAtIndex(int index)
    {
        return equips.ToArray()[index];
    }

    public int getIndexOfEquipment(Equipment equip)
    {
        return equips.IndexOf(equip);
    }

    public int GetDataCount()
    {
        return equips.Count;
    }

    public void RemoveData(int index)
    {
        equips.RemoveAt(index);
    }


}


[Serializable]
public class EquipData
{
    public string _name;
    public string _desc;
    public string _icon;
    public int _price;
    public bool _consumable, _consumed;
    public bool _useableOutsideBattle;
    public string _scope;
    public List<EffectData> _useEffects;
    public string _equipmentType;
    public string _equipmentPosition;
    public List<StatusData> _effects;
    public List<string> _requirements;
}