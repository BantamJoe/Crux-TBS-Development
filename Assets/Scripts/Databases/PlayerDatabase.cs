using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerDatabase {

    List<Player> players;


    public PlayerDatabase()
    {
        players = new List<Player>();
        loadData();
    }

    public void loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/playerDatabase.dat"))
        {
            players.Clear();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/playerDatabase.dat", FileMode.Open);
            List<PlayerData> data = (List<PlayerData>)formatter.Deserialize(file);
            //TODO: LOAD PLAYER DATA HERE
            file.Close();
        }
    }

    public void saveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/playerDatabase.dat", FileMode.OpenOrCreate);

        List<PlayerData> data = new List<PlayerData>();
        foreach (Player player in players)
        {
            PlayerData playData = new PlayerData();
            


            data.Add(playData);
        }

        formatter.Serialize(file, data);
        file.Close();
    }

    public void addPlayer(Player player)
    {
        players.Add(player);
    }

    public Player createPlayer(string username, BattleActor actor)
    {
        Player player = new Player();
        player.setUsername(username);
        player.setBattleActor(actor);
        return player;
    }

    public Player getPlayAtIndex(int index)
    {
        return players.ToArray()[index];
    }

    public int getIndexOfPlayer(Player player)
    {
        return players.IndexOf(player);
    }

    public int getDataCount()
    {
        return players.Count;
    }

    public void removePlayer(int index)
    {
        players.RemoveAt(index);
    }

}



[Serializable]

public class PlayerData
{

}






