 using UnityEngine;
using System.Collections;

public class EnemyMelee : EnemyBehaviour {

	public GameObject wall;
	Animator anim;

	public AudioClip meeleSound;

	void Awake()
	{
		wall = GameObject.Find ("Wall");
		anim = this.gameObject.GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		canAttack = true;
		moveSpeed = Random.Range (2.9f, 3.3f);
		GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, 0);

		gameObject.GetComponent<Renderer>().sortingOrder = -Mathf.RoundToInt (this.transform.position.y);

		LevelManager.enemiesRemaining++;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Wall")
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			anim.SetBool("Attacking", true);
			StartCoroutine(attack());
		}
	}

	public override IEnumerator attack()
	{
		if (meeleSound.loadState == AudioDataLoadState.Loaded)
		{
			anim.SetTrigger("Attack");
			GetComponent<AudioSource>().PlayOneShot(meeleSound);
			wall.GetComponent<Wall> ().Damage (attackDamage);
			yield return new WaitForSeconds (1);
		}
		if(canAttack) StartCoroutine (attack ());
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
