using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {
    Vector3 myLocation;
    GameObject thisObject;
    public GameObject graphic;

    public int currentChunk;

    public bool passable = false;
    public bool walkable = false;
    public int walkableDifficulty;

    public Actor()
    {
        determineMyLocation();
        if (walkableDifficulty == 0)
        {
            walkableDifficulty = 999;
        }
    }

    public Tile updateTile()
    {
        return null;
    }
    
    public bool isPassable()
    {
        return passable;
    }

    public bool isWalkable()
    {
        return walkable;
    }

    public int getWalkableDifficulty()
    {
        if (!walkable)
            return 999;
        return walkableDifficulty;
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    void determineMyLocation()
    {
        myLocation.Set(Mathf.FloorToInt(transform.position.x),
                       Mathf.FloorToInt(transform.position.y),
                       Mathf.FloorToInt(transform.position.z));
    }

    public Vector3 getMyLocation()
    {
        determineMyLocation();
        return myLocation;
    }

}
