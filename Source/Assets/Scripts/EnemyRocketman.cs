using UnityEngine;
using System.Collections;

public class EnemyRocketman : EnemyBehaviour {

	Animator anim;
		
	public LayerMask wallMask;

	public AudioClip gunSound;
	GameObject rocket;
	
	Vector2 targetPosition;
	
	bool firingPosition;
	bool firingBegan;
	
	public float fireDelay = 1f;
	
	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
		rocket = (GameObject)Resources.Load ("Rocket_1");

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
	
	Vector2 findWall()
	{
		Vector2 temp;
		RaycastHit2D hit = Physics2D.Raycast (this.transform.position, Vector2.right, 3000, wallMask.value);
		
		float offsetDistance = 0 - Random.Range (5f, 12f);
		
		Vector2 offset = new Vector2 (offsetDistance, 0);
		temp = (Vector2)hit.point + offset;
		
		return temp;
	}
	
	public override IEnumerator attack()
	{
		//if (gunSound.isReadyToPlay) audio.PlayOneShot(gunSound);
		yield return new WaitForSeconds (fireDelay);
		fireRocket ();
		if (canAttack) StartCoroutine (attack ());
	}

	void fireRocket()
	{
		GameObject missile = Instantiate (rocket, transform.position, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2 (3, 0);
	}
}
