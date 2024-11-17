using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Awake () {
		anim = this.GetComponent<Animator> ();
	}

	void Start() {
		StartCoroutine (explode());
	}

	public IEnumerator explode() {
		anim.SetTrigger ("explode");
		yield return new WaitForSeconds (0.5f);
		Destroy (this.gameObject);
	}
}