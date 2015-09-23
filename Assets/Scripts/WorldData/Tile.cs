using UnityEngine;

public class Tile : MonoBehaviour
{

    public bool passable;
    public bool walkable;
    public int difficulty;
    string material;

    public Tile()
    {
        passable = true;
        walkable = false;
    }

    public bool isPassable()
    {
        return passable;
    }
    
    public bool isWalkable()
    {
        return walkable;
    }

}
