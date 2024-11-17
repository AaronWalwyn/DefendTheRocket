using UnityEngine;
using System.Collections;

public class Astaria : MonoBehaviour {

	public GameObject moon;
	public LevelManager LM;
	public float offset;
	float alpha;

	void Start()
	{

	}
	// Update is called once per frame
	void Update () {

		float currentT = Time.time - LM.waveStart - offset;
		float endT = LM.waveEnd - LM.waveStart;
		alpha = currentT / endT;

		moon.GetComponent<Renderer>().material.color = new Color (moon.GetComponent<Renderer>().material.color.r, moon.GetComponent<Renderer>().material.color.g, moon.GetComponent<Renderer>().material.color.b, alpha);
	
	}
}
