using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ChunkDatabase : BaseDatabase
{

    public List<Chunk> chunks;

    private string filename = "Chunk";


    public ChunkDatabase()
    {
        chunks = new List<Chunk>();
        loadData();
    }

    public void loadData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/chunkDatabase.dat"))
        {
            chunks.Clear();
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/chunkDatabase.dat", FileMode.Open);
            List<ChunkData> data = (List<ChunkData>)formatter.Deserialize(file);
            foreach (ChunkData _data in data)
            {
                //LOAD DATA
            }
            file.Close();
        }
    }

    public void saveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/" + Database.Definitions().ModuleName + "/chunkDatabase.dat", FileMode.OpenOrCreate);

        List<ChunkData> data = new List<ChunkData>();
        foreach (Chunk chunk in chunks)
        {
            ChunkData chuData = new ChunkData();

            data.Add(chuData);
        }

        formatter.Serialize(file, data);
        file.Close();

    }

    public void addChunk(Chunk chunk)
    {
        chunks.Add(chunk);
    }

    

    public Chunk getChunkAtIndex(int index)
    {
        return chunks.ToArray()[index];
    }

    public int getIndexOfChunk(Chunk chunk)
    {
        return chunks.IndexOf(chunk);
    }

    public int GetDataCount()
    {
        return chunks.Count;
    }

    public void RemoveData(int index)
    {
        chunks.RemoveAt(index);
    }


}


[Serializable]
public class ChunkData
{
    List<bool> passable;
    List<bool> walkable;
    List<int> difficulty;
    List<string> material;
}