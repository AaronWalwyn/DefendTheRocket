using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	static public float maxHealth = 100;
	static public float health = 100;

	void Update()
	{
		if (health <= 0 && !GameManager.godMode)
		{
			if (GameOver.currentState == false)
			{
				GameObject.Find("_GM").GetComponent<GameOver>().endGame();
				//Destroy(this.gameObject);
			}
		}
	}

	public void Damage(float damageCoefficient)
	{
		health -= damageCoefficient;
	}
}
