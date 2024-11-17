using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public static int fuelMin = 4;
	public static int fuelMax = 14;
	public bool dead = false;

	float alpha = 1f;

	public float health = 100;

	//Number by which the score and credits is increased upon death.
	[SerializeField] int pointsWorth = 10;

	void Update()
	{
		//Fades out the enemy sprite to zero if the enemy is 'dead'

		if (health <= 0 && !dead)
		{
			dead = true;
			alpha += -1f * 1.1f * Time.deltaTime;
			this.gameObject.GetComponent<EnemyBehaviour>().death();
		}
		else if (health <= 0 && dead)
		{
			alpha += -1f * 1.1f * Time.deltaTime;
		}

		if(alpha <= 0 && dead)
		{
			Die ();
		}

		alpha = Mathf.Clamp01(alpha);
		GetComponent<Renderer>().material.color = new Color (GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, alpha);

		//If the enemy has been faded out and is not defined dead yet.

	}
	
	//Used to apply damage to the enemy.
	//@pram damageCoefficient
	public void Damage(float damageCoefficient)
	{
		health -= damageCoefficient;
	}
	
	//Function for when the enemy dies. Initiates the functions that drop fuel and drop
	//blueprints.
	//Finishes by destroying the GameObject.
	void Die()
	{
		this.GetComponent<Renderer>().enabled = false;

		LevelManager.enemiesRemaining--;

		if(Random.value < 0.6) DropFuel();
		if(Random.value < 0.025) Research.dropBlueprint();

		GameManager.score += pointsWorth;
		GameManager.credits += pointsWorth;

		Destroy(this.gameObject);
	}

	//Increments the amount of fuel the player has by a random value between the fuelMin
	//fuelMax values.
	void DropFuel()
	{
		GameManager.fuel += Mathf.RoundToInt(Random.Range(fuelMin, fuelMax));
	}
}
