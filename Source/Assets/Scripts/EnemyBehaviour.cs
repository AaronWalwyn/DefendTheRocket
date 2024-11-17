using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float attackDamage;
	public float moveSpeed;
	public bool canAttack;

	public bool fallOnDeath = false;
	//public Wall wall;

	public virtual IEnumerator attack()
	{
		yield return new WaitForSeconds (1);
	}

	public virtual void death()
	{
		Debug.Log ("Why won't you die!");
	}	
}
