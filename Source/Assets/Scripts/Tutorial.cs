using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	[SerializeField] UnityEngine.UI.Text tutorial;

	float alpha = 0f;
	public float fadeSpeed = 0.8f;
	private int fadeDir = -1;

	int count = 0;

	public IEnumerator beginTutorial()
	{
		LevelManager.tutorial = false;


		yield return new WaitForSeconds (0.5f);
		fadeDir = 1;
	}

 	void Update()
	{
		if(Input.GetMouseButtonUp(0))
			count++;

		if (count >= 2)
			fadeDir = -1;
	
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		tutorial.color = new Color (255f, 255f, 255f, alpha);
	}

}
