using UnityEngine;
using System.Collections.Generic;


public class Chunk {

    int chunkWidth, chunkHeight;

    public Tile[,,] chunkTiles;
    public List<Actor> actorList;

    public Chunk()
    {
        chunkWidth = Database.Definitions().chunkWidth;
        chunkHeight = Database.Definitions().chunkHeight;

        chunkTiles = new Tile[chunkWidth, chunkHeight, chunkWidth];

        for (int i = 0; i < chunkWidth; i++)
            for (int j = 0; j < chunkHeight; j++)
                for (int k = 0; k < chunkWidth; k++)
                    chunkTiles[i, j, k] = null;
        populateChunkListings();
    }

    public void populateChunkListings()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<Tile>() != null)
            {
                chunkTiles[(int)obj.transform.position.x,
                    (int)obj.transform.position.y,
                    (int)obj.transform.position.z] = obj.GetComponent<Tile>();
            }
            if(obj.GetComponent<Actor>() != null)
            {
                actorList.Add(obj.GetComponent<Actor>());
            }
        }
    }

    public Tile getTileAt(int x, int y, int z)
    {
        return chunkTiles[x, y, z];
    }
    public List<Actor> getActorsAtLocation(int x, int y, int z)
    {
        List<Actor> returnable = new List<Actor>();
        foreach (Actor act in actorList)
            if (act.getMyLocation() == new Vector3(x, y, z))
                returnable.Add(act);
        return returnable;
    }

    public bool doActorsBlockTile(int x, int y, int z)
    {
        foreach (Actor act in getActorsAtLocation(x, y, z))
        {
            if (!act.isPassable())
                return true;
        }
        return false;
    }

    public bool isWalkableActorAtTile(int x, int y, int z)
    {
        foreach (Actor act in getActorsAtLocation(x, y, z))
        {
            if (act.isWalkable())
                return true;
        }
        return false;
    }

    public List<BattleActor> getActorsWithinRange(Vector3 point, float range)
    {
        List<BattleActor> returnable = new List<BattleActor>();
        foreach (Actor act in actorList)
            if (Vector3.Distance(point, act.getMyLocation()) <= range)
            {
                if (act.GetComponent<BattleActor>() != null)
                    returnable.Add((BattleActor)act);
            }
        return returnable;
    }

    public bool isValidMoveWalking(Vector3 vec)
    {
        return isValidMoveWalking((int)vec.x, (int)vec.y, (int)vec.z);
    }

    public bool isValidMoveWalking(int x, int y, int z)
    {
        //		Debug.Log("CHECKING: " + x + "," + y + "," + z + " AND IT IS " + chunkTiles[x,y,z].isPassable());
        if (x < 0 || y < 0 || z < 0)
            return false;
        if (((chunkTiles[x, y, z] != null && chunkTiles[x, y, z].isWalkable())
            || isWalkableActorAtTile(x, y, z))
           && (chunkTiles[x, y + 1, z] == null || chunkTiles[x, y + 1, z].isPassable()))
            if (!doActorsBlockTile(x, y + 1, z))
                return true;
        return false;
    }

    public int getTileDifficulty(int x, int y, int z)
    {
        if (chunkTiles[x, y, z] == null)
        {
            if (!isWalkableActorAtTile(x, y, z))
                return 999;
            foreach (Actor act in getActorsAtLocation(x, y, z))
            {
                if (act.getWalkableDifficulty() == 999 || act.getWalkableDifficulty() == 0)
                    return 999;
                return act.getWalkableDifficulty();
            }
        }
        return chunkTiles[x, y, z].difficulty;
    }

}
