using UnityEngine;
using System.Collections;
using System.IO;

public static class IO {

	static Research rsch;

	static string saveVersion = "1.0";

	public static string dataPath;

	public static bool checkSaveVersion()
	{
		try
		{
			FileStream fs = new FileStream(dataPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			StreamReader sr = new StreamReader (fs);
			
			string versionCheck = sr.ReadLine(); //Check Version;

			sr.Close ();
			fs.Close ();

			if(versionCheck == GameManager.version)
				return true;
		} catch (IOException e) {
			Debug.Log (e.Message);
		}

		return false;
	}

	public static void loadGame()
	{
		try
		{
			FileStream fs = new FileStream(dataPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			StreamReader sr = new StreamReader (fs);

			string gameVersionCheck = sr.ReadLine();
			string saveversionCheck = sr.ReadLine(); //Check Version;
			Crew.clearCrew();
			LevelManager.tutorial = false;

			if(saveVersion.EndsWith(saveversionCheck))
			{
				GameManager.wave = int.Parse (sr.ReadLine ());
				GameManager.planet = int.Parse (sr.ReadLine ());
				Wall.health = int.Parse (sr.ReadLine ());
				Wall.maxHealth = int.Parse (sr.ReadLine ());
				
				Crew.engineer.noOf = int.Parse (sr.ReadLine ());
				Crew.scientist.noOf = int.Parse (sr.ReadLine ());
				Crew.mercenary.noOf = int.Parse (sr.ReadLine ());
				Crew.crewNum = int.Parse (sr.ReadLine ());
				Crew.maxCrew = int.Parse (sr.ReadLine ());
				Crew.totalWage = int.Parse (sr.ReadLine ());

				GameManager.score = int.Parse (sr.ReadLine ());
				GameManager.credits = int.Parse (sr.ReadLine ());
				GameManager.fuel = int.Parse (sr.ReadLine ());

				LevelManager.userDifficulty = float.Parse (sr.ReadLine ());
				Cheat.cheated = bool.Parse(sr.ReadLine ());
				LevelManager.enemiesRemaining = 0;

				Research.points = int.Parse (sr.ReadLine ());
				Research.currentResearch = int.Parse (sr.ReadLine());
				Weapons.weaponNum = int.Parse (sr.ReadLine());

				Rocket.createUpgrades();
				Weapons.createWeapons();
				Research.createBlueprints();
				
				for (int i=0; i < Research.blueprints.Length; i++)
				{
					Research.blueprints[i].dropped = bool.Parse (sr.ReadLine());
					Research.blueprints[i].researched = int.Parse (sr.ReadLine());
					Research.blueprints[i].unlock.setResearched(bool.Parse (sr.ReadLine()));
					Research.blueprints[i].unlock.purchased = bool.Parse (sr.ReadLine());
				}

				Weapons.setFirstWepon(Weapons.weaponNum);
				Application.LoadLevel (GameManager.planet);
			}
			else
			{
				Debug.Log ("This save file is incompatible");
			}
			
			sr.Close ();
			fs.Close ();
		} catch (IOException e) {

			Debug.Log (e.Message);
			Application.LoadLevel(0);
		}
	}

	public static void saveGame()
	{
		Debug.Log (Application.persistentDataPath + "/saveData.dat");
		
		try
		{
			FileStream fs = new FileStream(Application.persistentDataPath + "/saveData.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
			StreamWriter sw = new StreamWriter (fs);
			
			sw.WriteLine (GameManager.version);
			sw.WriteLine (saveVersion);
			
			sw.WriteLine (GameManager.wave);			//Wave
			sw.WriteLine (Application.loadedLevel);		//Planet
			
			sw.WriteLine (Wall.health);					//Health
			sw.WriteLine (Wall.maxHealth);				//Max Health
			
			sw.WriteLine (Crew.engineer.noOf);			//No. Engineers
			sw.WriteLine (Crew.scientist.noOf);			//No. Scientist
			sw.WriteLine (Crew.mercenary.noOf);			//No. Mercenaries
			sw.WriteLine (Crew.crewNum);
			sw.WriteLine (Crew.maxCrew);				//Max Crew No.
			sw.WriteLine (Crew.totalWage);
			
			sw.WriteLine (GameManager.score);			//Score
			sw.WriteLine (GameManager.credits);			//Credits
			sw.WriteLine (GameManager.fuel);			//Fuel
			
			sw.WriteLine (LevelManager.userDifficulty);
			sw.WriteLine (Cheat.cheated);
			
			sw.WriteLine (Research.points);				//Research Points
			
			sw.WriteLine (Research.currentResearch);	//Current Research
			sw.WriteLine (Weapons.weaponNum);			//Weapon Number
			
			//Saving of blueprints
			foreach (Research.Blueprint blueprint in Research.blueprints)
			{
				sw.WriteLine(blueprint.dropped);
				sw.WriteLine(blueprint.researched);
				sw.WriteLine(blueprint.unlock.getResearched());
				sw.WriteLine(blueprint.unlock.purchased);
			}
			
			sw.Close ();
			fs.Close ();
			
		} catch (IOException e) {
			
			Debug.Log (e.Message);
		}
	}

	public static void deleteSave()
	{
		if(File.Exists(IO.dataPath))
		{
			File.Delete(IO.dataPath);
		}
	}
}
