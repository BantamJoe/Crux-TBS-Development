using UnityEngine;
using System.Collections;

//The Database is a holder for the static Database.
//All information the game uses is referenced directly through Database

public class Database {
	
	private static Database database;

    private BattleActorDatabase battleActorDatabase;
    private ChunkDatabase chunkDatabase;
    private DefinitionsDatabase definitionsDatabase;
    private EffectDatabase effectDatabase;
    private EquipmentDatabase equipmentDatabase;
	private FormulaDatabase formulaDatabase;
    private ItemDatabase itemDatabase;
    private PlayerDatabase playerDatabase;
    private SkillDatabase skillDatabase;
    private StatusDatabase statusDatabase;

    private CruxGameManager gameManager;
    private BattleManager battleManager;
    

	private Database()
	{
		if (database != null) {
			Debug.Log("Database is trying to reinstantiate!");		
			return;
		}
		database = this;
		Init ();
	}
	
	public void Init()
	{
        definitionsDatabase = new DefinitionsDatabase();
        battleActorDatabase = new BattleActorDatabase();
        chunkDatabase = new ChunkDatabase();
        effectDatabase = new EffectDatabase();
        equipmentDatabase = new EquipmentDatabase();
        formulaDatabase = new FormulaDatabase();
        itemDatabase = new ItemDatabase();
        playerDatabase = new PlayerDatabase();
        skillDatabase = new SkillDatabase();
        statusDatabase = new StatusDatabase();
}
	
	public static Database getDatabase()
	{
		if (database == null) {
			new Database();		
		}
		return database;
	}
	
	public static void saveDatabase()
	{
        Database.getDatabase().definitionsDatabase.saveData();
        Database.getDatabase().battleActorDatabase.saveData();
        Database.getDatabase().chunkDatabase.saveData();
        Database.getDatabase().effectDatabase.saveData();
        Database.getDatabase().equipmentDatabase.saveData();
        Database.getDatabase().itemDatabase.saveData();
        Database.getDatabase().playerDatabase.saveData();
        Database.getDatabase().skillDatabase.saveData();
        Database.getDatabase().statusDatabase.saveData();
    }
	
	public static void loadDatabase()
	{
        Database.getDatabase().definitionsDatabase.loadData();
        Database.getDatabase().battleActorDatabase.loadData();
        Database.getDatabase().chunkDatabase.loadData();
        Database.getDatabase().effectDatabase.loadData();
        Database.getDatabase().equipmentDatabase.loadData();
        Database.getDatabase().itemDatabase.loadData();
        Database.getDatabase().playerDatabase.loadData();
        Database.getDatabase().skillDatabase.loadData();
        Database.getDatabase().statusDatabase.loadData();
    }

    public static BattleActorDatabase BattleActors()
    {
        return Database.getDatabase().battleActorDatabase;
    }

    public static ChunkDatabase Chunks()
    {
        return Database.getDatabase().chunkDatabase;
    }

    public static DefinitionsDatabase Definitions()
    {
        return Database.getDatabase().definitionsDatabase;
    }

    public static EffectDatabase Effects()
	{
		return Database.getDatabase().effectDatabase;
	}

    public static EquipmentDatabase Equipment()
    {
        return Database.getDatabase().equipmentDatabase;
    }

    public static FormulaDatabase Formula()
	{
		return Database.getDatabase ().formulaDatabase;
	}

    public static ItemDatabase Items()
    {
        return Database.getDatabase().itemDatabase;
    }

    public static PlayerDatabase Players()
    {
        return Database.getDatabase().playerDatabase;
    }

    public static SkillDatabase Skills()
    {
        return Database.getDatabase().skillDatabase;
    }

    public static StatusDatabase Statuses()
	{
		return Database.getDatabase().statusDatabase;
	}

    public static CruxGameManager GameManager()
    {
        return Database.getDatabase().gameManager;
    }

    public static BattleManager BattleManager()
    {
        return Database.getDatabase().battleManager;
    }


}
