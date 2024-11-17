using UnityEngine;
using System.Collections;

public class Research : MonoBehaviour {

	public class Blueprint {

		public bool dropped;
		public Unlock unlock;
		public float researchCost;
		public float researched;

		public Blueprint(Unlock unlock, int researchCost)
		{
			this.researched = 0;
			this.unlock = unlock;
			this.researchCost = researchCost;
			dropped = false;
		}
	}
	
	public static int points;

	public static bool newBlueprints = false;

	public static float coefficient = 3;

	public static Blueprint[] blueprints = new Blueprint[20];
	public static int currentResearch = -1;

	public static void createBlueprints()
	{
		//Astaria Blueprints - Level 001
		blueprints [0] = new Blueprint (Weapons.playerWeapons [1], 10); //Carmack Rifle
		blueprints [1] = new Blueprint (Rocket.shipUpgrades [0], 15); //Field Booster I
		
		//Patiess Blueprints - Level 002
		blueprints [2] = new Blueprint (Rocket.shipUpgrades [1], 20); //Crew Quaters I
		blueprints [3] = new Blueprint (Rocket.shipUpgrades [2], 25); //Ship Weapons I
		blueprints [4] = new Blueprint (Weapons.playerWeapons [2], 20); //M24 Assault Rifle
		
		//Kellia Blueprints - Level 003
		blueprints [5] = new Blueprint (Rocket.shipUpgrades [3], 30); //Laboratory
		blueprints [6] = new Blueprint (Rocket.shipUpgrades [4], 40); //Field Booster II
		blueprints [7] = new Blueprint (Weapons.playerWeapons [3], 35); //Zeus Rifle
		
		blueprints [8] = new Blueprint (Weapons.playerWeapons[4], 40); //Skull Crusher
		blueprints [9] = new Blueprint (Rocket.shipUpgrades [5], 90); //Crew Quaters II
		blueprints [10] = new Blueprint (Weapons.playerWeapons[5], 40); //Scar
		blueprints [11] = new Blueprint (Weapons.playerWeapons[6], 50); //Eden
		
		blueprints [12] = new Blueprint (Rocket.shipUpgrades [6], 60); //Ship Weapons II
		blueprints [13] = new Blueprint (Weapons.playerWeapons[7], 60);
		blueprints [14] = new Blueprint (Rocket.shipUpgrades [7], 60); //Field Booster III
		blueprints [15] = new Blueprint (Weapons.playerWeapons[8], 65);
		
		blueprints [16] = new Blueprint (Weapons.playerWeapons[9], 85);
		blueprints [17] = new Blueprint (Rocket.shipUpgrades [8], 75); //Refinery
		blueprints [18] = new Blueprint (Weapons.playerWeapons[10], 90);
		blueprints [19] = new Blueprint (Rocket.shipUpgrades [9], 80); //Crew Quaters III
	}

	public static void UpdateReseach()
	{
		points += Mathf.RoundToInt (Random.Range(0, GameManager.wave));
		blueprints [currentResearch].researched = points;
		points = 0;

		if(blueprints [currentResearch].researched >= blueprints[currentResearch].researchCost)
		{
			blueprints [currentResearch].researched = blueprints[currentResearch].researchCost;
			blueprints [currentResearch].unlock.research();

			if (blueprints [currentResearch].unlock.GetType() == typeof(Weapons.Gun))
				GameManager.newWeapon = true;
			else
				GameManager.newShipUpgrade = true;

		}
	}

	public static void dropBlueprint()
	{
		int value = -1;

		switch (Application.loadedLevel)
		{
			case 1: 
				value = Random.Range (0, 2);
				break;
			case 2:
				value = Random.Range (2, 5);
				break;
			case 3: 
				value = Random.Range (5, 8);
				break;
			case 4:
				value = Random.Range (8, 12);
				break;
			case 5: 
				value = Random.Range (12, 16);
				break;
			case 6:
				value = Random.Range (16, 20);
				break;
		}

		if(!blueprints[value].dropped)
		{
			GameManager.newBlueprint = true;
			blueprints [value].dropped = true;
			newBlueprints = true;
		}
	}
}
