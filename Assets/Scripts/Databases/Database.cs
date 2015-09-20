using UnityEngine;
using System.Collections;

//The Database is a holder for the static Database.
//All information the game uses is referenced directly through Database

public class Database {
	
	private static Database database;
	
	private EffectDatabase effectDatabase;
	private StatusDatabase statusDatabase;
	private FormulaDatabase formulaDatabase;
	private DefinitionsDatabase definitionsDatabase;
	
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
		effectDatabase = new EffectDatabase ();
		statusDatabase = new StatusDatabase ();
		formulaDatabase = new FormulaDatabase ();
		definitionsDatabase = new DefinitionsDatabase ();
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
		Database.getDatabase().effectDatabase.saveData();
	//	Database.getDatabase ().statusDatabase.saveData ();
	}
	
	public static void loadDatabase()
	{
		Database.getDatabase ().effectDatabase.loadData ();
	//	Database.getDatabase ().statusDatabase.loadData ();
	}
	
	public static EffectDatabase Effects()
	{
		return Database.getDatabase().effectDatabase;
	}
	
	public static FormulaDatabase Formula()
	{
		return Database.getDatabase ().formulaDatabase;
	}
	
	public static DefinitionsDatabase Definitions()
	{
		return Database.getDatabase ().definitionsDatabase;
	}
	
	public static StatusDatabase Statuses()
	{
		return Database.getDatabase().statusDatabase;
	}
	
}
