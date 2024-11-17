using UnityEngine;
using System.Collections;

public class EnemySuicide : EnemyBehaviour {

	public GameObject wall;
	Animator anim;
	
	public float blastRadius = 2.5f;

	public float moveMin;
	public float moveMax;

	void Awake()
	{
		wall = GameObject.Find ("Wall");
		anim = this.gameObject.GetComponent<Animator> ();
	}
	
	// Use this for initialization
	void Start () {
		moveSpeed = Random.Range (moveMin, moveMax);
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
		
		LevelManager.enemiesRemaining++;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Wall")
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			anim.SetBool("targetReached", true);
			this.gameObject.GetComponent<EnemyHealth>().Damage(9999999);
		}
	}

	void Update()
	{
		if (this.gameObject.GetComponent<EnemyHealth>().health <= 0)
		{
			Collider2D[] hits = Physics2D.OverlapCircleAll (transform.position, blastRadius);

			foreach(Collider2D hit in hits)
			{
				if(hit.tag == "Wall")
				{
					hit.GetComponent<Wall>().Damage(attackDamage);
				}
				else if (hit.tag == "Enemy")
				{
					hit.GetComponent<EnemyHealth>().Damage(attackDamage);
				}
			}
		}
	}

	public override void death()
	{
		canAttack = false;
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
		Destroy(this);
	}
}
