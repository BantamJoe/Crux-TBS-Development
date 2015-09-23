using UnityEngine;
using System.Collections;

public class CruxGameManager : MonoBehaviour {

    public Player player;
    string GameState;
    public Chunk currentChunk;
    //public BattleManager battleManager;
    
	// Use this for initialization
	void Start () {
        GameState = "Exploration";
        
	}

    public void loadGameData(Player _player, BattleActor _actor)
    {
        player = _player;
        player.setBattleActor(_actor);
    }

    public void setPlayer(Player _player)
    {
        player = _player;
    }

    public void setPlayerBattleActor(BattleActor _actor)
    {
        player.setBattleActor(_actor);
    }

    public string getGameState()
    {
        return GameState;
    }

    public void StartBattle(Actor instigator)
    {
        GameState = "Battle";
    }

    public void endBattle()
    {
        GameState = "Exploration";
    }

    public void input(int input)
    {
        if (GameState.Equals("Battle"))
        {
            //SEND TO BATTLE MANAGER
        }
        if (GameState.Equals("Exploration"))
        {
            //SEND TO EXPLORATION MANAGER
        }

    }

    public void determineMove(int dir, bool jumpToggle)
    {
        int movX, movZ;
        movX = movZ = 0;
        switch (dir)
        {
            case 1: movX = 1; break;
            case 2: movZ = -1; break;
            case 3: movX = -1; break;
            case 4: movZ = 1; break;
        }
        Vector3 destVec =
            new Vector3((int)(player.getBattleActor().getMyLocation().x + movX / Database.Definitions().tileWidth),
                        (int)(player.getBattleActor().getMyLocation().y / Database.Definitions().tileHeight),
                        (int)(player.getBattleActor().getMyLocation().z + movZ / Database.Definitions().tileWidth));
        if (!jumpToggle && currentChunk.isValidMoveWalking(destVec))
        {

            iTween.MoveTo(player.getBattleActor().getGameObject(),
                              iTween.Hash("position", destVec,
                            "easetype", "easeInOutQuad", //easeInOutQuad
                            "time", 1));
        }

        if (jumpToggle)
        {
            for (int i = (int)player.getBattleActor().getMaxJump() ; i > (0 - (int)player.getBattleActor().getMaxJump()) ; i--)
            {
                if (destVec.y + i >= 0 && currentChunk.isValidMoveWalking((destVec + new Vector3(0, i, 0))))
                {
                    destVec.y += i;
                    iTween.MoveTo(player.getBattleActor().getGameObject(),
                                  iTween.Hash("position", destVec,
                                "easetype", "easeInOutQuad", //easeInOutQuad
                                "time", 1));

                    break;
                }

            }
        }
    }


}
