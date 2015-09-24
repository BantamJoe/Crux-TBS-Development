using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour {

	public Image playerImage;
	public Text playerName;
	public BattleActor actor;

	UIManager uiManager;

	public void setUIManager(UIManager manager)
	{
		uiManager = manager;
	}

	public void setBattleActor(BattleActor _actor)
	{
		actor = _actor;
	}

	public BattleActor getBattleActor()
	{
		return actor;
	}

	public void updateStats()
	{
		playerName.text = actor.name;
	}

	public void onClick()
	{
		uiManager.createCharacterPanel(actor);
	}

}
