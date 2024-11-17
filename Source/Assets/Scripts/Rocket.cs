using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	public class Upgrade : Unlock {

		public Upgrade(string name)
		{
			this.setName (name);
			this.purchased = false;
		}

		public Upgrade()
		{
			this.setName ("....");
		}
	}

	public class FieldBooster : Upgrade {

		int upgradeAmount;

		public FieldBooster(string name, int upgradeAmount, int cost, string description)
		{
			this.setName (name);
			this.upgradeAmount = upgradeAmount;
			this.cost = cost;
			this.description = description;
		}

		public override void purchase()
		{
			if(!purchased && GameManager.credits >= cost && this.getResearched())
			{
				GameManager.credits -= cost;
				Wall.maxHealth += upgradeAmount;
				Wall.health = Wall.maxHealth;
				purchased = true;
			}
		}

	}

	public class CrewQuaters : Upgrade {

		int upgradeAmount;

		public CrewQuaters(string name, int upgradeAmount, int cost, string description)
		{
			this.setName (name);
			this.upgradeAmount = upgradeAmount;
			this.cost = cost;
			this.description = description;
		}
		
		public override void purchase()
		{
			if(!purchased && GameManager.credits >= cost && this.getResearched())
			{
				GameManager.credits -= cost;
				Crew.maxCrew += upgradeAmount;
				purchased = true;
			}
		}
	}

	public class ShipWeapons : Upgrade {
		
		int upgradeAmount;
		
		public ShipWeapons(string name, int upgradeAmount, int cost, string description)
		{
			this.setName (name);
			this.upgradeAmount = upgradeAmount;
			this.cost = cost;
			this.description = description;
		}

		public override void purchase()
		{
			if(!purchased && GameManager.credits >= cost && this.getResearched())
			{
				GameManager.credits -= cost;
				purchased = true;
				Rocket.weaponTier += upgradeAmount;
			}
		}
	}

	public class Laboratory : Upgrade {

		float upgradeAmount;

		public Laboratory(string name, float upgradeAmount, int cost, string description)
		{
			this.setName (name);
			this.upgradeAmount = upgradeAmount;
			this.cost = cost;
			this.description = description;
		}
		
		public override void purchase()
		{
			if(!purchased && GameManager.credits >= cost && this.getResearched())
			{
				GameManager.credits -= cost;

				Research.coefficient = upgradeAmount;

				purchased = true;
			}
		}
	}

	public class Refinery : Upgrade {
		
		float upgradeAmount;
		
		public Refinery(string name, float upgradeAmount, int cost, string description)
		{
			this.setName (name);
			this.upgradeAmount = upgradeAmount;
			this.cost = cost;
			this.description = description;
		}
		
		public override void purchase()
		{
			if(!purchased && GameManager.credits >= cost && this.getResearched())
			{
				GameManager.credits -= cost;
				
				EnemyHealth.fuelMin = (int)(EnemyHealth.fuelMin * upgradeAmount);
				EnemyHealth.fuelMax = (int)(EnemyHealth.fuelMax * upgradeAmount);
				
				purchased = true;
			}
		}
	}

	public static GameObject[] Cannons = new GameObject[3];

	public static Upgrade[] shipUpgrades = new Upgrade[10];

	public static int weaponTier = 0;

	// Use this for initialization
	void Start () { 
		if(Application.loadedLevel != 0)
		{
			assignCannons();
			renderCannons();
		}

	}
	
	public static void createUpgrades()
	{
		shipUpgrades [0] = new FieldBooster ("Field Booster I", 150, 750, "This field booster upgrade will allow you to hold off pesky aliens for that little bit longer.");
		shipUpgrades [1] = new CrewQuaters ("Crew Quarters I", 10, 950, "You found a spare room all you need is to buy some beds and you could fit in ten more crew."); 
		shipUpgrades [2] = new ShipWeapons ("Ship Weapons I", 1, 1000, "You know what's better then one gun? Two guns! This upgrade adds another turret to your ship."); 
		shipUpgrades [3] = new Laboratory ("Laboratory", 2, 1250, "This state of the art laboratory allows your scientists to work even faster to unlock blueprints.");
		shipUpgrades [4] = new FieldBooster ("Field Booster II", 350, 1450, "Those aliens just don't stop! This field upgrade will help you keep them at bay.");
		shipUpgrades [5] = new CrewQuaters ("Crew Quarters II", 10, 1675, "Still need a few extra hands? If we add some bunk beds maybe we can fit in another ten.");
		shipUpgrades [6] = new ShipWeapons ("Ship Weapons II", 1, 1750, "You know what's better then two guns? You got it... Three guns!");
		shipUpgrades [7] = new FieldBooster ("Field Booster III", 500, 1850, "This is the latest forcefield technology, if this doesn't stop them nothing will!");
		shipUpgrades [8] = new Refinery ("Refinery", 1.5f, 1950, "This refinery allows you to make more rocket fuel then ever before, you'll never be stuck on a planet again."); /* Needs Class Creating */
		shipUpgrades [9] = new CrewQuaters ("Crew Quarters III", 10, 2125, "You want more crew? Well clear out the HR department and you can put ten more beds in there.");
	}

	public void assignCannons()
	{
		Cannons [0] = transform.FindChild ("cannon_1").gameObject;
		Cannons [1] = transform.FindChild ("cannon_2").gameObject;
		Cannons [2] = transform.FindChild ("cannon_3").gameObject;
	}

	public void renderCannons()
	{
		for(int i=0; i <= weaponTier; i++)
		{
			if (Cannons [i] != null)
				Cannons [i].GetComponent<Renderer>().enabled = true;
		}
	}

	public void fireTurret()
	{
		Random.Range (0, weaponTier);
		Cannons [Random.Range (0, weaponTier + 1)].GetComponent<Animator>().SetTrigger("fire");
	}
}
