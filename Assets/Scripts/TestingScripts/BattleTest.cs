using UnityEngine;
using System.Collections;

public class BattleTest : MonoBehaviour {
    public BattleActor one, two;
    public Chunk chunk;
	// Use this for initialization
	public void Start () {
        Database.loadDatabase();
        Player player = new Player();
        player.setUsername("MY USERNAME");
        player.setBattleActor(one);

        Database.GameManager().setPlayer(player);

        Database.GameManager().currentChunk =
            chunk;

        //this.GetComponent<UIManager>().updateUI();


	}
	
}
