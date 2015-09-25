using UnityEngine;
using System.Collections.Generic;


public class BattleManager : MonoBehaviour {
    public Player player;

    public List<BattleActor> battleList;
    public List<BattleActor> turnOrder;

    Chunk currentChunk;

    BattleActor activeActor;

    bool myTurn;

    public BattleManager()
    {
        myTurn = false;
        battleList = new List<BattleActor>();
        turnOrder = new List<BattleActor>();

    }

    public void updateBattleManagerInformation()
    {
        currentChunk = Database.GameManager().currentChunk;
        player = Database.GameManager().player;
    }

    public void AddBattleActor(BattleActor _battleActor)
    {
        Debug.Log("Adding Battle Actor");
        battleList.Add(_battleActor);
    }

    public void RemoveBattleActor(BattleActor _battleActor)
    {
        turnOrder.Remove(_battleActor);
        battleList.Remove(_battleActor);
        if (!checkBattleOver().Equals("NONE"))
        {
            battleList.Clear();
            turnOrder.Clear();
            Database.GameManager().endBattle();
        }
    }

    public void determineTurnOrder()
    {
        turnOrder = battleList;
        activeActor = turnOrder[0];//ToArray()[0];
    }

    public void nextTurn()
    {
        turnOrder.Add(turnOrder[0]);
        turnOrder.RemoveAt(0);
        activeActor = turnOrder[0];

        determineActive();

    }

    void determineActive()
    {
        if (activeActor == Database.GameManager().player.getBattleActor())
        {
            setActive(true);
        }
        else
        {
            setActive(false);
        }
    }

    void setActive(bool _active)
    {
        myTurn = _active;
    }

    public bool isItMyTurn()
    {
        determineActive();
        return myTurn;
    }

    public void clearBattleInformation()
    {
        battleList.Clear();
        turnOrder.Clear();
    }

    public void determineMove(int dir, bool jumpToggle)
    {
        determineActive();
        if (!myTurn) { return; }
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
            new Vector3((int)(activeActor.getMyLocation().x + movX / Database.Definitions().tileWidth),
                        (int)(activeActor.getMyLocation().y / Database.Definitions().tileWidth),
                        (int)(activeActor.getMyLocation().z + movZ / Database.Definitions().tileWidth));
        if (!jumpToggle && currentChunk.isValidMoveWalking(destVec))
        {
            if (currentChunk.getTileDifficulty((int)destVec.x, (int)(destVec.y), (int)destVec.z) <= activeActor.getStat("currentStamina"))
            {
                activeActor.modifyStat("currentstamina", Mathf.RoundToInt(0 - currentChunk.getTileDifficulty((int)destVec.x, (int)(destVec.y), (int)destVec.z)));
                //uiManager.UpdateGUI();
                iTween.MoveTo(activeActor.getGameObject(),
                              iTween.Hash("position", destVec,
                            "easetype", "easeInOutQuad", //easeInOutQuad
                            "time", 1));
            }
        }

        if (jumpToggle)
        {
            for (int i = (int)activeActor.getMaxJump() ; i > (0 - (int)activeActor.getMaxJump()) ; i--)
            {
                if (destVec.y + i >= 0 && currentChunk.isValidMoveWalking((destVec + new Vector3(0, i, 0))))
                {
                    if (currentChunk.getTileDifficulty((int)destVec.x, (int)(destVec.y + i), (int)destVec.z) <= activeActor.getStat("currentStamina"))
                    {
                        //player.modifyBasicStat("stamina", Mathf.RoundToInt(0 - currentChunk.getTileDifficulty((int)destVec.x, (int)(destVec.y+i), (int)destVec.z)));
                        activeActor.modifyStat("currentstamina", Mathf.RoundToInt(0 - currentChunk.getTileDifficulty((int)destVec.x, (int)(destVec.y + i), (int)destVec.z)));
                        //uiManager.UpdateGUI();
                        destVec.y += i;
                        iTween.MoveTo(activeActor.getGameObject(),
                                      iTween.Hash("position", destVec,
                                    "easetype", "easeInOutQuad", //easeInOutQuad
                                    "time", 1));
                    }
                    break;
                }
            }
        }


    }



    public bool useSkill(Vector3 targetPos, Skill skill)
    {
        if (currentChunk.getActorsAtLocation((int)targetPos.x, (int)targetPos.y, (int)targetPos.z).Count == 0
            || !skill.canCast(activeActor))
            return false;
        List<BattleActor> deadActors = new List<BattleActor>();
        foreach (BattleActor actor in currentChunk.getActorsAtLocation((int)targetPos.x, (int)targetPos.y, (int)targetPos.z))
        {
            //Debug.Log ("USING SKILL");
            skill.useSkill(activeActor, actor);
            if (!actor.getHasTurn())
            {
                deadActors.Add(actor);
            }
        }
        foreach (BattleActor actor in deadActors)
        {
            RemoveBattleActor(actor);
        }
        deadActors.Clear();

        return true;
    }

    public bool useItem(Item item)
    {
        item.useItem(activeActor);
        return true;
    }

    public string checkBattleOver()
    {
        string firstBattler = battleList.ToArray()[0].team;
        for (int i = 1 ; i < battleList.Count ; i++)
        {
            if (!battleList.ToArray()[i].team.Equals(firstBattler))
            {
                return "NONE";
            }
        }
        return firstBattler;
    }

    public BattleActor getActiveActor()
    {
        return activeActor;
    }




}
