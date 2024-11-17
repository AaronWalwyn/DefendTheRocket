using UnityEngine;
using System.Collections;

public class EnemyGrenadier : EnemyBehaviour {

	Animator anim;
	Wall wall;
	EnemyHealth health;

	public LayerMask wallMask;

	Vector2 targetPosition;

	public GameObject missile;
	
	bool throwingPosition;
	bool throwingBegan;

	public float xV = 10.5f, yV = 0f;

	public float throwDelay = 2.5f;

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
		wall = GameObject.Find ("Wall").GetComponent<Wall> ();
		health = this.gameObject.GetComponent<EnemyHealth> ();
		
		canAttack = true;
		
		targetPosition = new Vector2(findWall().x - Random.Range(6f, 12f), 0);
		throwingPosition = false;
		throwingBegan = false;
	}

	void Awake()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
		LevelManager.enemiesRemaining++;
	}

	void Update () {
		if(transform.position.x >= targetPosition.x && !health.dead)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			anim.SetBool("targetReached", true);
			throwingPosition = true;
		}
		
		if (throwingPosition && !throwingBegan)
		{
			throwingBegan = true;
			yV = getYVelocity();
			StartCoroutine (attack());
		}
	}

	public Vector2 findWall()
	{
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right, 9999f, wallMask.value);

		return hit.point;
	}

	public float getYVelocity()
	{
		//(9.8 * Tx) / 2 where Tx = x / Vx
		float x = findWall ().x - transform.position.x;
		float Tx = x / xV;
		return 4.9f * Tx;
	}

	public override IEnumerator attack()
	{
		yield return new WaitForSeconds (throwDelay);

		if (canAttack)
		{
			GameObject thrownMissile = (GameObject)Instantiate (missile, this.transform.position, Quaternion.identity);
			thrownMissile.GetComponent<Rigidbody2D>().velocity = new Vector2 (xV, yV);
			
			anim.SetTrigger ("fire");
			wall.Damage (attackDamage);

			StartCoroutine (attack ());
		}
	}
	
	public override void death()
	{
		canAttack = false;
		anim.SetBool ("dead", true);
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);

		if (fallOnDeath)
			GetComponent<Rigidbody2D>().isKinematic = false;
	}
}
