using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class GameManager  : MonoBehaviour {

	public static string version = "1.0.5";

	public static int wave = 0;
	public static int planet;

	public static int score;
	public static int credits;
	public static int fuel;

	public static bool newBlueprint = false;
	public static bool newWeapon = false;
	public static bool newShipUpgrade = false;

	public static int reseachPoints;

	[SerializeField] UnityEngine.Canvas OutWaveOptions;

	public GUISkin waveOptionsSkin;

	public static string tip;

	public static bool allWaveOptions;
	bool runDevMenu;

	public static Planets[] planets = new Planets[6];

	public static bool godMode = false;

	void OnLevelWasLoaded()
	{
		Camera.main.GetComponent<AudioListener> ().enabled = MainMenu.gameAudio;
	}

	// Use this for initialization
	void Awake () {
		runDevMenu = false;

		planets [0] = new Planets ("Astaria", "A primitive planet of reptiles, never before have they been visited by aliens. The planets technology is significantly lacking, their technology is based around gunpowder and animals.");
		planets [1] = new Planets ("Patiess", "This is a test of how long the box can be written in. The quick brown fox jumped over the laxy dog");
		planets [2] = new Planets ("Kellia", "Kellia is a inhabited by synthetic AIs, created by Rhea. Exiled since their attempted uprising, the AIs have become very protective of their new homeworld.");
		planets [3] = new Planets ("Dune", "This vast desert planet is home to a race of hardened warriors, what they lack in resources they make up for in brute strength");
		planets [4] = new Planets ("Maia", "Maia was once covered in lush rainforest and plants, then the outlaws and criminals exiled from Rhea descended upon it, over years of faction warfare Maia now resembles a swampland.");
		planets [5] = new Planets ("Rhea", "A technologically advanced, militaristic planet, inhabited by one of the few surviving human colonies in the area. Their military uses bionic enhancments to upgrade their abilites.");

		OutWaveOptions.enabled = false;
	}
	
	public void waveOptions()
	{
		allWaveOptions = true;

		Debug.Log ("Run Wave Options");
		OutWaveOptions.enabled = true;
		OutWaveOptions.gameObject.GetComponent<GUIOutWaveOptions> ().showWaveOptionsMenu ();
		GameObject.Find ("UpgradeRocket").GetComponent<GUIUpgradeRocket> ().updateButtons ();

		tip = GameTips.getGameTip ();

		credits -= Crew.totalWage;
		if (Research.currentResearch != -1) Research.UpdateReseach ();
	}

	public void devMenu()
	{
		runDevMenu = true;
	}

	void OnGUI()
	{
		Vector2 ratio = new Vector2(Screen.width / 2048f , Screen.height / 1536f );
		Matrix4x4 guiMatrix = Matrix4x4.identity;
		guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
		GUI.matrix = guiMatrix;

		GUI.skin = waveOptionsSkin;
		GUI.depth = -1000;

		if(runDevMenu)
		{
			//Close Dev Menu
			GUI.Box(new Rect(70, 70, 1908, 583), "Dev Menu");
			if (GUI.Button(new Rect(1908, 70, 70, 70), "[X]")) runDevMenu = false;

			GUI.Label(new Rect(1500, 200, 400, 65), "Enemies: " + LevelManager.enemiesRemaining);
			GUI.Label(new Rect(1500, 280, 400, 65), "Delay: " + LevelManager.latestSpawnDelay);
			GUI.Label(new Rect(1500, 360, 400, 65), "Tier: " + Rocket.weaponTier);

			//Turn God mode on or off, default is off
			GUI.Label(new Rect(140, 140, 200, 66), "God    Mode    :");
			if (GUI.Button(new Rect(370, 140, 66, 66), "-")) godMode = false;
			if (GUI.Button(new Rect(470, 140, 66, 66), "+")) godMode = true;

			//Increment or decrement the current wave, with immediate effect.
			GUI.Label(new Rect(140, 220, 200, 66), "Wave    :");
			if (GUI.Button(new Rect(370, 220, 66, 66), "-")) wave--;
			if (GUI.Button(new Rect(470, 220, 66, 66), "+")) wave++;

			//Run the end of wave options
			GUI.Label(new Rect(140, 300, 200, 66), "runWaveOptions    :");
			if (GUI.Button(new Rect(470, 300, 66, 66), "+")) 
			{
				allWaveOptions = true;
			}

			//Increment or decrement cresits by 100
			GUI.Label(new Rect(140, 380, 200, 66), "Money    :");
			if (GUI.Button(new Rect(370, 380, 66, 66), "-")) credits -= 1000;
			if (GUI.Button(new Rect(470, 380, 66, 66), "+")) credits += 1000;

			GUI.Label(new Rect(140, 460, 200, 66), "Fuel    :");
			if (GUI.Button(new Rect(370, 460, 66, 66), "-")) fuel += 100;
			if (GUI.Button(new Rect(470, 460, 66, 66), "+")) fuel += 100;

			GUI.Label(new Rect(140, 540, 200, 66), "Health    :");
			if (GUI.Button(new Rect(370, 540, 66, 66), "-")) repairForcefield(-10, 0);
			if (GUI.Button(new Rect(470, 540, 66, 66), "+")) repairForcefield(10, 0);

			//Add or remove mercenaries
			GUI.Label(new Rect(606, 140, 200, 66), "Mercenaries    :");
			if (GUI.Button(new Rect(836, 140, 66, 66), "-")) Crew.mercenary.fire ();
			if (GUI.Button(new Rect(936, 140, 66, 66), "+")) Crew.mercenary.hire ();

			//Add or remove mercenaries
			GUI.Label(new Rect(606, 220, 200, 66), "Scientist    :");
			if (GUI.Button(new Rect(836, 220, 66, 66), "-")) Crew.scientist.fire ();
			if (GUI.Button(new Rect(936, 220, 66, 66), "+")) Crew.scientist.hire ();

			//Add or remove mercenaries
			GUI.Label(new Rect(606, 300, 200, 66), "Engineer    :");
			if (GUI.Button(new Rect(836, 300, 66, 66), "-")) Crew.engineer.fire ();
			if (GUI.Button(new Rect(936, 300, 66, 66), "+")) Crew.engineer.hire ();

			//Add or remove mercenaries
			GUI.Label(new Rect(606, 380, 200, 66), "Research    :");
			if (GUI.Button(new Rect(836, 380, 66, 66), "-")) Research.points -= 100;
			if (GUI.Button(new Rect(936, 380, 66, 66), "+")) Research.points += 100;

			//Add or remove mercenaries
			GUI.Label(new Rect(606, 460, 200, 66), "UnlockAll    :");
			if (GUI.Button(new Rect(936, 460, 66, 66), "+")) unlockAll();

			//Add or remove mercenaries
			GUI.Label(new Rect(606, 540, 200, 66), "Cannon Tier    :");
			if (GUI.Button(new Rect(836, 540, 66, 66), "-")) Rocket.weaponTier -= 1;
			if (GUI.Button(new Rect(936, 540, 66, 66), "+")) Rocket.weaponTier += 1;
		}

		GUI.matrix = Matrix4x4.identity;
	}

	public IEnumerator nextWave()
	{
		float fadetime = this.GetComponent<Fading> ().beginFade (1);
		yield return new WaitForSeconds(fadetime);

		OutWaveOptions.enabled = false;

		allWaveOptions = false;
		fuel -= enoughFuel(Application.loadedLevel, planet);

		if (Application.loadedLevel == planet)
		{
			GameObject.Find ("_LM").GetComponent<LevelManager> ().newWave ();
			fadetime = this.GetComponent<Fading> ().beginFade (-1);
		}
		else
			Application.LoadLevel(planet);
	}

	public static int enoughFuel(int from, int to)
	{
		int[,] travelDistance =  new int[,] {{000,200,250,225,325,300},
											 {200,000,275,175,400,300},
											 {250,275,000,475,080,500},
											 {225,175,475,000,480,050},
											 {325,400,080,480,000,550},
											 {300,300,500,050,550,000}};
		return travelDistance [from - 1, to - 1];
	}

	void repairForcefield(int coefficient, int cost)
	{
		if (cost <= credits)
		{
			Wall.health += coefficient;
			credits -= cost;
		}

		if(Wall.health > Wall.maxHealth)
			Wall.health = Wall.maxHealth;
	}

	public static void saveGame()
	{
		Debug.Log (Application.persistentDataPath + "/saveData.dat");
		IO.saveGame ();
	}

	public static void unlockAll()
	{
		foreach(Research.Blueprint blueprint in Research.blueprints)
		{
			blueprint.dropped = true;
			blueprint.researched = blueprint.researchCost;
			blueprint.unlock.setResearched(true);
		}
	}
}
