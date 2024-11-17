using UnityEngine;
using System.Collections;

public class EnemyVehicleLand : EnemyBehaviour {

	Animator anim;
	Wall wall;

	public AudioClip gunSound;
	
	public LayerMask wallMask;
	public int burst;
	
	Vector2 targetPosition;
	
	bool firingPosition;
	bool firingBegan;
	
	public float fireDelay = 5f;
	public float burstDelay = 0.360f;
	
	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
		wall = GameObject.Find ("Wall").GetComponent<Wall> ();
		
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
		if(transform.position.x >= targetPosition.x)
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
		RaycastHit2D hit = Physics2D.Raycast (this.transform.position, Vector2.right, 3000, wallMask.value);
		
		float offsetDistance = 0 - Random.Range (3f, 14f);
		
		Vector2 offset = new Vector2 (offsetDistance, 0);
		temp = (Vector2)hit.point + offset;
		
		return temp;
	}
	
	
	public override IEnumerator attack()
	{
		yield return new WaitForSeconds (fireDelay);

		anim.SetTrigger ("fire");
		for(int i=0; i<burst; i++)
		{
			if (gunSound.loadState == AudioDataLoadState.Loaded)
			{
				wall.Damage (attackDamage);
				GetComponent<AudioSource>().PlayOneShot(gunSound);
				yield return new WaitForSeconds (burstDelay);
			}
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
