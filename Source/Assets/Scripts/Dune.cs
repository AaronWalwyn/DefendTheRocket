using UnityEngine;
using System.Collections;

public class Dune : MonoBehaviour {

	public GameObject sandCloud;
	LevelManager LM;
	public float offset;
	float alpha;

	// Use this for initialization
	void Start () {
		LM = GameObject.Find ("_LM").GetComponent<LevelManager>();
		Achievements.travelToDune ();
	}
	
	// Update is called once per frame
	void Update () {

		float currentT = Time.time - LM.waveStart - offset;
		float endT = LM.waveEnd - LM.waveStart;
		alpha = 1f - (currentT / endT);
		
		sandCloud.GetComponent<Renderer>().material.color = new Color (sandCloud.GetComponent<Renderer>().material.color.r, 
		                                               sandCloud.GetComponent<Renderer>().material.color.g, 
		                                               sandCloud.GetComponent<Renderer>().material.color.b, 
		                                               alpha);
	
	}
}
