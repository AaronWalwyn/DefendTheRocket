using UnityEngine;
using System.Collections;

public class EnemyGunman : EnemyBehaviour {

	Animator anim;
	Wall wall;
	EnemyHealth health;

	public AudioClip gunSound;

	public LayerMask wallMask;

	Vector2 targetPosition;

	bool firingPosition;
	bool firingBegan;

	public float fireDelay = 5f;

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
		wall = GameObject.Find ("Wall").GetComponent<Wall> ();
		health = this.gameObject.GetComponent<EnemyHealth> ();

		canAttack = true;

		targetPosition = findWall ();
		firingPosition = false;
		firingBegan = false;
	}

	void Awake()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);
		LevelManager.enemiesRemaining++;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x >= targetPosition.x && !health.dead)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
			anim.SetBool("targetReached", true);
			firingPosition = true;
		}

		if (firingPosition && !firingBegan)
		{
			firingBegan = true;
			StartCoroutine (attack());
		}
	}

	public Vector2 findWall()
	{
		Vector2 temp;
		RaycastHit2D hit = Physics2D.Raycast (this.transform.position, Vector2.right, 300f, wallMask.value);

		float offsetDistance = 0 - Random.Range (1f, 12f);
		
		Vector2 offset = new Vector2 (offsetDistance, 0);
		temp = (Vector2)hit.point + offset;
		
		return temp;
	}
	

	public override IEnumerator attack()
	{
		yield return new WaitForSeconds (fireDelay);

		if (gunSound.loadState == AudioDataLoadState.Loaded)
		{
			GetComponent<AudioSource>().PlayOneShot(gunSound);
			anim.SetTrigger ("fire");
			wall.Damage (attackDamage);
		}

		if (canAttack) StartCoroutine (attack ());
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
