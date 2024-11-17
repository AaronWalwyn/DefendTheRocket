using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public GameObject explosion;

	public float missileDamage = 7f;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Wall")
		{
			//col.gameObject.GetComponent<Wall>().Damage(missileDamage);
			//explode();
			shootDown();
		}
	}

	public void shootDown()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll (transform.position, 0.9f);

		foreach(Collider2D hit in hits)
		{
			if(hit.tag == "Enemy")
			{
				hit.gameObject.GetComponent<EnemyHealth>().Damage(30);
			}
			else if (hit.tag == "Wall")
			{
				hit.gameObject.GetComponent<Wall>().Damage(5);
			}
		}
		//Debug.Log ("Missile Shot Down");
		explode ();
	}

	void explode()
	{
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}
