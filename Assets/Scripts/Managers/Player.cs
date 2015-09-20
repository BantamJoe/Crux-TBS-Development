using UnityEngine;
using System.Collections;

public class Player {
	
	string username;
	BattleActor actor;
	
	public Player()
	{
		actor = new BattleActor ();
	}
	
	public void setUsername(string _username)
	{
		username = _username;
	}
	
	public string getUsername()
	{
		return username;
	}
	
	public void setBattleActor(BattleActor _actor)
	{
		actor = _actor;
	}
	
	public BattleActor getBattleActor()
	{
		return actor;
	}
}
