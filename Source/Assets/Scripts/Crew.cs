using UnityEngine;
using System.Collections;

/**
 * Super class for managing all the crew and the roles that crew members perform during each wave.
 * Also manages the wages of the crew, how many crew can be purchased and something else.
 */
public class Crew : MonoBehaviour {

	[SerializeField] LayerMask enemyLayer;

	public static int totalWage;

	public static int crewNum;
	public static int maxCrew = 20;

	public class CrewType {

		public int noOf = 0;
		public string description;
		public int cost;
		public int wage;
		public int damage;

		public CrewType(string d, int c, int w)
		{
			this.description = d;
			this.cost = c;
			this.wage = w;
		}

		public CrewType(string d, int c, int w, int da)
		{
			this.description = d;
			this.cost = c;
			this.wage = w;
			this.damage = da;
		}

		public void hire()
		{
			if(crewNum < maxCrew && GameManager.credits >= engineer.cost)
			{
				this.noOf++;
				crewNum++;
				GameManager.credits -= this.cost;
				totalWage += this.wage;
			}
		}

		public void fire()
		{
			if(this.noOf > 0)
			{
				this.noOf--;
				crewNum--;
				totalWage -= this.wage;
			}
		}
	}

	public static CrewType engineer = new CrewType(
		"Is your forcefield taking a bit of a beating? Engineers will help repair your forcefield, even when underfire.",
		105, 65);

	public static CrewType scientist = new CrewType(
		"Found yourself a blueprint? Shame those squiggles mean nothing to you, hire a scientist and they may know what is means.",
		125, 75);

	public static CrewType mercenary = new CrewType(
		"Need a bit of help wielding all that fire power on your ship, hiring mercenaries will allow you to make use of your ships weapons.",
		95, 50, 999);


	// Use this for initialization
	public void startCrew()
	{
		StartCoroutine (repairForcefield ());
		StartCoroutine (fireWeapon());
		StartCoroutine (research ());
	}

	public static void clearCrew()
	{
		engineer.noOf = 0;
		scientist.noOf = 0;
		mercenary.noOf = 0;

		crewNum = 0;
		totalWage = 0;
	}

	//Method allowing the forcefield to be repaired by engineers
	IEnumerator repairForcefield()
	{
		//Only if the wall is damaged, the level (wave) is not completed and the No. Engineers is more then
		//zero otherwise there is a division by zero.
		if (engineer.noOf > 0 && !LevelManager.levelComplete && !GameOver.currentState)
		{
			if (Wall.health < Wall.maxHealth)
			{
				Wall.health++;
			}
			yield return new WaitForSeconds (5f / (float)engineer.noOf); //Waits for 5 seconds divided by the number of Engineers.
			StartCoroutine (repairForcefield ());	//Calls self
		}
		yield return new WaitForSeconds (0);
	}

	//Method for allowing the mercenaries to fire the ships turrets.
	IEnumerator fireWeapon()
	{
		if (mercenary.noOf > 0 && !LevelManager.levelComplete && !GameOver.currentState)
		{
			Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(0,0), 10f, enemyLayer.value);
			if(hits.Length > 0)
			{
				int value = Mathf.RoundToInt (Random.Range(0, hits.Length));
				hits[value].gameObject.GetComponent<EnemyHealth>().Damage(mercenary.damage);
				LevelManager.rocket.GetComponent<Rocket>().fireTurret();
			}

			yield return new WaitForSeconds (10f / (float)mercenary.noOf / (1f + (0.3f * Rocket.weaponTier)));
			StartCoroutine (fireWeapon ());
		}

		yield return new WaitForSeconds (0);
	}

	IEnumerator research()
	{
		if(!LevelManager.levelComplete && !GameOver.currentState)
		{
			if(scientist.noOf > 0)
				Research.points += Mathf.RoundToInt(scientist.noOf * Research.coefficient);

			yield return new WaitForSeconds (15);
			StartCoroutine (research());
		}

		yield return new WaitForSeconds (0);
	}
}
